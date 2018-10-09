using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Effects
{
    public class Effect : IHasRequirements
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        public int Type { get; }
        
        /// <summary>
        /// The potency of the effect
        /// </summary>
        public float Potency { get; }

        /// <summary>
        /// The applicable requirements for this effect to be active
        /// </summary>
        public IEnumerable<Requirement> Requirements { get; }

        public Effect(int type, float potency, IEnumerable<Requirement> requirements = null)
        {
            Type = type;
            Potency = potency;
            Requirements = requirements ?? new Requirement[0];
        }
    }
}