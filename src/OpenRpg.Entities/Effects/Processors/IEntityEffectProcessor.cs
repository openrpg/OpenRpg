using OpenRpg.Entities.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public interface IEntityEffectProcessor<in T> where T : EntityData
    {
        ComputedEffects ComputeEffects(T entity);
    }
}