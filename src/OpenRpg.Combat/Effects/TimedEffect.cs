using System.Linq;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Effects
{
    public class TimedEffect : NamedEffect
    {
        /// <summary>
        /// An allowed amount of stacks for this effect
        /// </summary>
        /// <remarks>
        /// Most effects will not be stackable, but this allows you to have multiple stacks of same buff, so whenever
        /// you cant to calculate the proc/effect you can factor this stacking in
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

        public TimedEffect() { }

        /// <summary>
        /// Allows you to populate the base data from an existing effect
        /// </summary>
        /// <param name="baseEffect">The effect to copy from</param>
        public TimedEffect(Effect baseEffect)
        {
            EffectType = baseEffect.EffectType;
            Requirements = baseEffect.Requirements.ToArray();
            Potency = baseEffect.Potency;
        } 
    }
}