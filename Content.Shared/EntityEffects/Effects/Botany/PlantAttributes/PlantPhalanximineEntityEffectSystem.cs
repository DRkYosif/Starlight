using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that mutates plant to lose health with time.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantPhalanximineEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantPhalanximine>
{
    [Dependency] private readonly PlantHolderSystem _plantHolder = default!;
    [Dependency] private readonly PlantTraitsSystem _plantTrait = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantPhalanximine> args)
    {
        if (!_plantTray.TryGetPlant(entity.AsNullable(), out var plant))
            return;

        _plantTrait.DelTrait(entity.Owner, new TraitUnviable());
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantPhalanximine : EntityEffectBase<PlantPhalanximine>
{
    public override string EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        Loc.GetString("entity-effect-guidebook-plant-phalanximine", ("chance", Probability));
}
