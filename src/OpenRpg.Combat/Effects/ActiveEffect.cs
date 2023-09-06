namespace OpenRpg.Combat.Effects
{
    /// <summary>
    /// Represents an active
    /// </summary>
    public class ActiveEffect
    {
        /// <summary>
        /// The underlying timed effect that is active
        /// </summary>
        public TimedEffect Effect { get; set; }

        /// <summary>
        /// The amount of stacks of this effect active
        /// </summary>
        public int Stacks { get; set; } = 1;
        
        /// <summary>
        /// The amount of time the effect has been active in seconds
        /// </summary>
        public float ActiveTime { get; set; }
        
        /// <summary>
        /// Time since the last tick in seconds
        /// </summary>
        public float TimeSinceTick { get; set; }

        public ActiveEffect()
        {}

        public ActiveEffect(TimedEffect effect)
        { Effect = effect; }
    }
}