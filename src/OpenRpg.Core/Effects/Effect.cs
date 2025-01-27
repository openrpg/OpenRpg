using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Effects
{
    /// <summary>
    /// The effect object indicates a type of effect with a potency that can be applied to anything
    /// </summary>
    /// <remarks>
    /// Generally some parent object like a Character will have various effects all combined together to drive the
    /// stats, but can be used any way required
    /// </remarks>
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
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}