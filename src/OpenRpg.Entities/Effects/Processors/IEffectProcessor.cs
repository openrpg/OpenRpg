using System.Collections.Generic;
using BaseEntity = OpenRpg.Entities.Entity.Entity;

namespace OpenRpg.Entities.Effects.Processors
{
    public interface IEffectProcessor
    {
        IEnumerable<StaticEffect> ComputeEffects(IHasEffects context, BaseEntity relatedEntity = null);
        IEnumerable<StaticEffect> ComputeEffects(BaseEntity entity);
    }
}