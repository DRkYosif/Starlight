using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that adjusts the health of a plant.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantAdjustHealthEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantAdjustHealth>
{
    [Dependency] private PlantHolderSystem _plantHolder = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantAdjustHealth> args)
    {
        if (!_plantTray.TryGetPlant(entity.AsNullable(), out var plant))
            return;

        if (!TryComp<PlantHolderComponent>(plant, out var plantHolder))
            return;

        if (plantHolder.Dead)
            return;

        plantHolder.Health += args.Effect.Amount;
        _plantHolder.CheckHealth((plant.Value, null));
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantAdjustHealth : BasePlantAdjustAttribute<PlantAdjustHealth>
{
    public override string GuidebookAttributeName { get; set; } = "plant-attribute-health";
}
