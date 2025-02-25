using System.Collections.Generic;

namespace OpenRpg.Entities.Effects.Processors
{
    public class ComputedEffects
    {
        public Dictionary<int, float> EffectResults { get; set; } = new Dictionary<int, float>();
        public List<DeferredEffect> DeferredEffects { get; set; } = new List<DeferredEffect>();
    }
}