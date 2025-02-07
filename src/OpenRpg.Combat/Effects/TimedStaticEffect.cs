using System.Linq;
using OpenRpg.Combat.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Combat.Effects
{
    /// <summary>
    /// Describes an transient/temporary effect that can be applied to an entity for a given period of time
    /// </summary>
    /// <remarks>
    /// This just describes how the effect should be processed, and is more akin to a template. It should be
    /// used in conjunction with ActiveEffect for actually tracking and processing the effect.
    /// </remarks>
    public class TimedStaticEffect : StaticEffect, IHasDataId, IHasLocaleDescription, IHasVariables<ITimedEffectVariables>
    {
        /// <inheritdoc />
        public int Id { get; set; }
        
        /// <inheritdoc />
        public string NameLocaleId { get; set; }

        /// <inheritdoc />
        public string DescriptionLocaleId { get; set; }
        
        /// <summary>
        /// An allowed amount of stacks for this effect
        /// </summary>
        /// <remarks>
        /// Most effects will not be stackable, but this allows you to have multiple stacks of same buff, so whenever
        /// you want to calculate the proc/effect you can factor this stacking in
        /// </remarks>
        public int MaxStack { get; set; } = 1;
        
        /// <summary>
        /// The duration this effect should last in seconds
        /// </summary>
        /// <remarks>1.5 would be 1.5 seconds</remarks>
        public float Duration { get; set; }
        
        /// <summary>
        /// The frequency this buff should proc in seconds
        /// </summary>
        /// <remarks>If there is no frequency then it is seen as always active</remarks>
        public float Frequency { get; set; }
        
        /// <summary>
        /// Any additional variables to store against this timed effect
        /// </summary>
        public ITimedEffectVariables Variables { get; set; } = new DefaultTimedEffectVariables();

        public TimedStaticEffect() { }

        /// <summary>
        /// Allows you to populate the base data from an existing effect
        /// </summary>
        /// <param name="baseStaticEffect">The effect to copy from</param>
        public TimedStaticEffect(StaticEffect baseStaticEffect)
        {
            EffectType = baseStaticEffect.EffectType;
            Requirements = baseStaticEffect.Requirements.ToArray();
            Potency = baseStaticEffect.Potency;
        }
    }
}