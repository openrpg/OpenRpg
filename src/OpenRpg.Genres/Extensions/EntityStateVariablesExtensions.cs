using System;
using System.Linq;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.State.Entity;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStateVariablesExtensions
    {
        public static int Health(this IEntityStateVariables state) => (int)state.Get(GenreEntityStateVariableTypes.Health);
        public static void Health(this IEntityStateVariables state, int value) => state[GenreEntityStateVariableTypes.Health] = value;
        
        public static void AddHealth(this IEntityStateVariables state, int change, int? maxHealth = null)
        {
            var newValue = state.Health() + change;
            if(newValue <= 0) { newValue = 0; }
            
            if(maxHealth == null) 
            { state.Health(newValue); }
            else 
            { state.AddValue(GenreEntityStateVariableTypes.Health, newValue, 0, maxHealth.Value); }
        }

        public static void DeductHealth(this IEntityStateVariables state, int change, int? maxHealth = null)
        {
            var newValue = state.Health() - change;
            if(newValue <= 0) { newValue = 0; }

            if (maxHealth == null)
            { state.Health(newValue); }
            else
            { state.AddValue(GenreEntityStateVariableTypes.Health, newValue, 0, maxHealth.Value); }
        }
        
        public static void ApplyDamageToTarget(this IEntityStateVariables state, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);
            if (totalDamage < 0) { totalDamage = 0; }
            state.DeductHealth(totalDamage);
        }
        
        public static bool IsDead(this IEntityStateVariables state)
        { return state.Health() <= 0; }
    }
}