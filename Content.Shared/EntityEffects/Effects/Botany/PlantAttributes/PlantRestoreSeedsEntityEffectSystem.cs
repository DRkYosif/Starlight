using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;
using Content.Shared.Botany.Traits.Components;
using Content.Shared.Popups;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that restores ability to get seeds from plant seed maker.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantRestoreSeedsEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantRestoreSeeds>
{
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly PlantHolderSystem _plantHolder = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantRestoreSeeds> args)
    {
        if (!_plantTray.HasPlantAlive(entity.AsNullable()))
            return;

        _popup.PopupEntity(Loc.GetString("botany-plant-seedsrestored"), entity);
        RemCompDeferred<PlantTraitSeedlessComponent>(entity.Owner);
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantRestoreSeeds : EntityEffectBase<PlantRestoreSeeds>
{
    public override string EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        Loc.GetString("entity-effect-guidebook-plant-seeds-add", ("chance", Probability));
}
