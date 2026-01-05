using Content.Shared.Botany.Components;
using Content.Shared.Botany.Systems;

namespace Content.Shared.EntityEffects.Effects.Botany.PlantAttributes;

/// <summary>
/// Entity effect that adjusts the mutation level of a plant.
/// </summary>
/// <inheritdoc cref="EntityEffectSystem{T,TEffect}"/>
public sealed partial class PlantAdjustMutationLevelEntityEffectSystem : EntityEffectSystem<PlantTrayComponent, PlantAdjustMutationLevel>
{
    [Dependency] private PlantHolderSystem _plantHolder = default!;

    protected override void Effect(Entity<PlantTrayComponent> entity, ref EntityEffectEvent<PlantAdjustMutationLevel> args)
    {
        if (entity.Comp.PlantEntity == null || Deleted(entity.Comp.PlantEntity))
            return;

        var plantUid = entity.Comp.PlantEntity.Value;
        if (!TryComp<PlantHolderComponent>(plantUid, out var plantHolder))
            return;

        plantHolder.MutationLevel += args.Effect.Amount * plantHolder.MutationMod;
        _plantHolder.CheckHealth(plantUid);
    }
}

/// <inheritdoc cref="EntityEffect"/>
public sealed partial class PlantAdjustMutationLevel : BasePlantAdjustAttribute<PlantAdjustMutationLevel>
{
    public override string GuidebookAttributeName { get; set; } = "plant-attribute-mutation-level";
}
