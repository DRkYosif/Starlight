using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that sets plant potency.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantAdjustPotencyEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantAdjustPotency>
{
    [Dependency] private readonly PlantSystem _plant = default!;
    [Dependency] private readonly PlantTraySystem _plantTray = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantAdjustPotency> args)
    {
        if (!_plantTray.HasPlantAlive(entity.AsNullable()))
            return;

        var plantUid = entity.Comp.PlantEntity!.Value;
        if (!TryComp<PlantComponent>(plantUid, out var plant))
            return;

        _plant.AdjustPotency((plantUid, plant), args.Effect.Amount);
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantAdjustPotency : BasePlantAdjustAttribute<PlantAdjustPotency>
{
    public override string GuidebookAttributeName { get; set; } = "plant-attribute-potency";
}
