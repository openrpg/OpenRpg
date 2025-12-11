using BaseEntity = OpenRpg.Entities.Entity.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public interface IEntityEffectProcessor<in T> where T : BaseEntity
    {
        ComputedEffects ComputeEffects(T entity);
    }
}