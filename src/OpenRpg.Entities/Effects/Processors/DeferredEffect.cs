using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Entities.Effects.Processors
{
    public class DeferredEffect
    {
        public ScaledEffect ScaledEffect { get; set; }
        public IReadOnlyCollection<IEffect> Context { get; set; }
    }
}