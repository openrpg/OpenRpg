using BaseEntity = OpenRpg.Entities.Entity.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public interface IEffectProcessor<in T> where T : BaseEntity
    {
        ComputedEffects ComputeEffects(T entity);
    }
}