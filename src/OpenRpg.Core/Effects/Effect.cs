using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Effects
{
    public class Effect : IHasRequirements
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        public int EffectType { get; }
        
        /// <summary>
        /// The potency of the effect
        /// </summary>
        public float Potency { get; }

        /// <summary>
        /// The applicable requirements for this effect to be active
        /// </summary>
        public IEnumerable<Requirement> Requirements { get; }

        public Effect(int effectType, float potency, IEnumerable<Requirement> requirements = null)
        {
            EffectType = effectType;
            Potency = potency;
            Requirements = requirements ?? new Requirement[0];
        }
    }
}