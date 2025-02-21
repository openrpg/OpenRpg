using System;
using System.Collections.Generic;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Scaling;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Effects
{
    /// <summary>
    /// An effect that computes the potency based off some scaling value
    /// </summary>
    public class ScaledEffect : IEffect
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        public int EffectType { get; set; }
        
        /// <summary>
        /// The type of value required for scaling, i.e level, index etc
        /// </summary>
        public int ScalingType { get; set; }
        
        /// <summary>
        /// Used for certain scaling types
        /// </summary>
        public int ScalingIndex { get; set; }

        /// <summary>
        /// The function that calculates the potency of the effect
        /// </summary>
        public ScalingFunction PotencyFunction { get; set; } = new ScalingFunction(PresetCurves.Linear, 1, 100, 1, 20);

        /// <summary>
        /// The applicable requirements for this effect to be active
        /// </summary>
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}