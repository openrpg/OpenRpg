using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Effects
{
    public class Effect : IHasRequirements
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        public int EffectType { get; set; }
        
        /// <summary>
        /// The potency of the effect
        /// </summary>
        public float Potency { get; set; }

        /// <summary>
        /// The applicable requirements for this effect to be active
        /// </summary>
        public IEnumerable<Requirement> Requirements { get; set; } = new List<Requirement>();
    }
}