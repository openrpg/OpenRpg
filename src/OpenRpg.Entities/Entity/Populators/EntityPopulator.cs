using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Extensions;

namespace OpenRpg.Entities.Entity.Populators
{
    public class EntityPopulator<T> : IEntityPopulator<T> where T : Entity
    {
        public IEntityStatPopulator StatPopulator { get; }
        public IEntityStatePopulator StatePopulator { get; }
        public IEntityEffectProcessor<T> EntityEffectProcessor { get; }

        public EntityPopulator(IEntityStatPopulator statPopulator, IEntityStatePopulator statePopulator, IEntityEffectProcessor<T> entityEffectProcessor)
        {
            StatPopulator = statPopulator;
            StatePopulator = statePopulator;
            EntityEffectProcessor = entityEffectProcessor;
        }

        public void PopulateStateAndState(T entity, ComputedEffects computedEffects, bool refreshState)
        {
            StatPopulator.Populate(entity.Stats, computedEffects, null);
            
            if(refreshState)
            { StatePopulator.Populate(entity.State, computedEffects, new [] { entity.Stats }); }
        }

        public void Populate(T entity, bool refreshState = false)
        {
            var computedEffects = EntityEffectProcessor.ComputeEffects(entity);
            PopulateStateAndState(entity, computedEffects, refreshState);
            
            if (computedEffects.DeferredEffects.Count == 0)
            { return; }
    
            // If we have deferred effects process them then run again with processed effects
            computedEffects.ProcessDeferredEffects(entity);
            PopulateStateAndState(entity, computedEffects, refreshState);
        }
    }
}