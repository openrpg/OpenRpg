using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Effects
{
    public class ActiveEffect
    {
        /// <summary>
        /// The timed effect details
        /// </summary>
        public TimedEffect Effect { get; set; }
        
        /// <summary>
        /// The amount of time the effect has been active in seconds
        /// </summary>
        public float ActiveTime { get; set; }
        
        /// <summary>
        /// Time since the last tick in seconds
        /// </summary>
        public float TimeSinceTick { get; set; }
    }
}