using OpenRpg.Core.Effects;

namespace OpenRpg.Entities.Effects.Processors
{
    public class DeferredEffect
    {
        public ScaledEffect ScaledEffect { get; set; }
        public IHasEffects Context { get; set; }
    }
}