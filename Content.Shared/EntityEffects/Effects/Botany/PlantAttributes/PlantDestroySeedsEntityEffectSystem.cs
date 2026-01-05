using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;
using Content.Shared.Popups;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that removes ability to get seeds from plant using seed maker.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantDestroySeedsEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantDestroySeeds>
{
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly PlantHolderSystem _plantHolder = default!;
    [Dependency] private readonly PlantTraitsSystem _plantTraits = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantDestroySeeds> args)
    {
        if (!_plantTray.TryGetPlant(entity.AsNullable(), out var plant))
            return;

        _popup.PopupEntity(
            Loc.GetString("botany-plant-seedsdestroyed"),
            entity,
            PopupType.SmallCaution
        );
        _plantTraits.AddTrait(entity.Owner, new TraitSeedless());
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantDestroySeeds : EntityEffectBase<PlantDestroySeeds>
{
    public override string EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        Loc.GetString("entity-effect-guidebook-plant-seeds-remove", ("chance", Probability));
}
