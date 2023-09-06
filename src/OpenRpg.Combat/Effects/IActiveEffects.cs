using System;
using System.Collections.Generic;
using OpenRpg.Combat.Variables;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Effects
{
    /// <summary>
    /// The active effects allows us to wrap up concerns for managing active effects and abstracting away certain details
    /// like the stacking etc
    /// </summary>
    public interface IActiveEffects : IHasEffects
    {
        event EventHandler<ActiveEffect> EffectAdded;
        event EventHandler<ActiveEffect> EffectTriggered;
        event EventHandler<ActiveEffect> EffectExpired;
        
        /// <summary>
        /// All active effects
        /// </summary>
        IReadOnlyCollection<ActiveEffect> ActiveEffects { get; }
        
        /// <summary>
        /// Variables specific to the active effects
        /// </summary>
        IActiveEffectsVariables Variables { get; }
        
        /// <summary>
        /// This will attempt to add an effect, each implementation may differ how stacking is handled etc
        /// </summary>
        bool AddEffect(TimedEffect effect);

        /// <summary>
        /// Updates the effects based off how much time has passed
        /// </summary>
        /// <param name="elapsedTime">how much time has passed in seconds as a float i.e 1.5f is 1.5 seconds</param>
        void UpdateEffects(float elapsedTime);
        
        /// <summary>
        /// Attempts to remove the timed effect from the active effects
        /// </summary>
        bool RemoveEffect(int timedEffectId);
        
        /// <summary>
        /// Gets the effect by the timed effect id in the inventory
        /// </summary>
        /// <param name="timedEffectId">The timed effect id</param>
        /// <returns>The active effect instance or null</returns>
        ActiveEffect GetEffect(int timedEffectId);

        /// <summary>
        /// Checks to see if the given timed effect is active
        /// </summary>
        /// <param name="timedEffectId">The id of the timed effect</param>
        /// <returns>True if it has the timed effect false if not</returns>
        bool HasEffect(int timedEffectId);
    }
}