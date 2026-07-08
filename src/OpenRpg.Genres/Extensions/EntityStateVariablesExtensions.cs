using System;
using System.Linq;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Core.Extensions;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStateVariablesExtensions
    {
        extension(EntityStateVariables state)
        {
            public int Health
            {
                get => (int)state.Get(GenreEntityStateVariableTypes.Health);
                set => state[GenreEntityStateVariableTypes.Health] = value;
            }
            
            public bool IsDead => state.Health <= 0;
        }
        
        public static void AddHealth(this EntityStateVariables state, int change, int? maxHealth = null)
        {
            if (maxHealth == null)
            {
                var newValue = state.Health + change;
                if (newValue <= 0) newValue = 0;
                state.Health = newValue;
            }
            else
            { state.AddValue(GenreEntityStateVariableTypes.Health, change, 0, maxHealth.Value); }
        }

        public static void DeductHealth(this EntityStateVariables state, int change, int? maxHealth = null)
        {
            if (maxHealth == null)
            {
                var newValue = state.Health - change;
                if (newValue <= 0) newValue = 0;
                state.Health = newValue;
            }
            else
            { state.AddValue(GenreEntityStateVariableTypes.Health, -change, 0, maxHealth.Value); }
        }
        
        public static void RestoreLife(this EntityStateVariables state, int amount, int? maxHealth = null)
        { state.Health = Math.Max(1, maxHealth.HasValue ? Math.Min(amount, maxHealth.Value) : amount); }
        
        public static void ApplyDamageToTarget(this EntityStateVariables state, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);
            if (totalDamage < 0) { totalDamage = 0; }
            state.DeductHealth(totalDamage);
        }
        
        extension(EntityStateVariables state)
        {
            public int Stamina
            {
                get => (int)state.Get(GenreEntityStateVariableTypes.Stamina);
                set => state[GenreEntityStateVariableTypes.Stamina] = value;
            }
        }
        
        public static void AddStamina(this EntityStateVariables state, int change, int? maxStamina = null)
        {
            if (maxStamina == null)
            {
                var newValue = state.Stamina + change;
                if (newValue <= 0) newValue = 0;
                state.Stamina = newValue;
            }
            else
            { state.AddValue(GenreEntityStateVariableTypes.Stamina, change, 0, maxStamina.Value); }
        }
        
        public static void DeductStamina(this EntityStateVariables state, int change, int? maxStamina = null)
        {
            if (maxStamina == null)
            {
                var newValue = state.Stamina - change;
                if (newValue <= 0) newValue = 0;
                state.Stamina = newValue;
            }
            else
            { state.AddValue(GenreEntityStateVariableTypes.Stamina, -change, 0, maxStamina.Value); }
        }
    }
}