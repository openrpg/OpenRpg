using System.Collections.Generic;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStatVariableExtensions
    {
        extension(EntityStatsVariables state)
        {
            public int MaxHealth
            {
                get => (int)state.Get(GenreEntityStatsVariableTypes.MaxHealth);
                set => state[GenreEntityStatsVariableTypes.MaxHealth] = value;
            }
            
            public int MaxStamina
            {
                get => (int)state.Get(GenreEntityStatsVariableTypes.MaxStamina);
                set => state[GenreEntityStatsVariableTypes.MaxStamina] = value;
            }
            
            public float HealthRegen
            {
                get => state.Get(GenreEntityStatsVariableTypes.HealthRegen);
                set => state[GenreEntityStatsVariableTypes.HealthRegen] = value;
            }
            
            public float StaminaRegen
            {
                get => state.Get(GenreEntityStatsVariableTypes.StaminaRegen);
                set => state[GenreEntityStatsVariableTypes.StaminaRegen] = value;
            }
            
            public float HealthRegenRate
            {
                get => state.Get(GenreEntityStatsVariableTypes.HealthRegenRate);
                set => state[GenreEntityStatsVariableTypes.HealthRegenRate] = value;
            }
            
            public float StaminaRegenRate
            {
                get => state.Get(GenreEntityStatsVariableTypes.StaminaRegenRate);
                set => state[GenreEntityStatsVariableTypes.StaminaRegenRate] = value;
            }
            
            public float Damage
            {
                get => state.Get(GenreEntityStatsVariableTypes.Damage);
                set => state[GenreEntityStatsVariableTypes.Damage] = value;
            }
            
            public float Defense
            {
                get => state.Get(GenreEntityStatsVariableTypes.Defense);
                set => state[GenreEntityStatsVariableTypes.Defense] = value;
            }
            
            public float CriticalDamageChance
            {
                get => state.Get(GenreEntityStatsVariableTypes.CriticalDamageChance);
                set => state[GenreEntityStatsVariableTypes.CriticalDamageChance] = value;
            }
            
            public float CriticalDamageMultiplier
            {
                get => state.Get(GenreEntityStatsVariableTypes.CriticalDamageMultiplier);
                set => state[GenreEntityStatsVariableTypes.CriticalDamageMultiplier] = value;
            }
            
            public float CooldownReduction
            {
                get => state.Get(GenreEntityStatsVariableTypes.CooldownReduction);
                set => state[GenreEntityStatsVariableTypes.CooldownReduction] = value;
            }
            
            public float AttackSize
            {
                get => state.Get(GenreEntityStatsVariableTypes.AttackSize);
                set => state[GenreEntityStatsVariableTypes.AttackSize] = value;
            }
            
            public float AttackRange
            {
                get => state.Get(GenreEntityStatsVariableTypes.AttackRange);
                set => state[GenreEntityStatsVariableTypes.AttackRange] = value;
            }
            
            public float MovementSpeed
            {
                get => state.Get(GenreEntityStatsVariableTypes.MovementSpeed);
                set => state[GenreEntityStatsVariableTypes.MovementSpeed] = value;
            }
        }
    }
}