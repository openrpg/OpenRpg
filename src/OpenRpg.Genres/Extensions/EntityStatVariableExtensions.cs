using System.Collections.Generic;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityStatVariableExtensions
    {
        public static int MaxHealth(this EntityStatsVariables stats) => (int)stats.Get(GenreEntityStatsVariableTypes.MaxHealth);
        public static void MaxHealth(this EntityStatsVariables stats, int value) => stats[GenreEntityStatsVariableTypes.MaxHealth] = value;
        
        public static int MaxStamina(this EntityStatsVariables stats) => (int)stats.Get(GenreEntityStatsVariableTypes.MaxStamina);
        public static void MaxStamina(this EntityStatsVariables stats, int value) => stats[GenreEntityStatsVariableTypes.MaxStamina] = value;
        
        public static float Damage(this EntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Damage);
        public static void Damage(this EntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Damage] = value;
        public static float Defense(this EntityStatsVariables stats) => stats.Get(GenreEntityStatsVariableTypes.Defense);
        public static void Defense(this EntityStatsVariables stats, float value) => stats[GenreEntityStatsVariableTypes.Defense] = value;
        
        public static float CriticalDamageChance(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.CriticalDamageChance, 0);
        public static void CriticalDamageChance(this EntityStatsVariables stats, float criticalDamageChance) => stats[GenreEntityStatsVariableTypes.CriticalDamageChance] = criticalDamageChance;
        public static float CriticalDamageMultiplier(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.CriticalDamageMultiplier, 0);
        public static void CriticalDamageMultiplier(this EntityStatsVariables stats, float criticalDamageMultiplier) => stats[GenreEntityStatsVariableTypes.CriticalDamageMultiplier] = criticalDamageMultiplier;

        public static float CooldownReduction(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.CooldownReduction, 0);
        public static void CooldownReduction(this EntityStatsVariables stats, float cooldownReduction) => stats[GenreEntityStatsVariableTypes.CooldownReduction] = cooldownReduction;
        
        public static float AttackSize(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.AttackSize, 0);
        public static void AttackSize(this EntityStatsVariables stats, float attackSize) => stats[GenreEntityStatsVariableTypes.AttackSize] = attackSize;
        
        public static float AttackRange(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.AttackRange, 0);
        public static void AttackRange(this EntityStatsVariables stats, float attackSpeed) => stats[GenreEntityStatsVariableTypes.AttackRange] = attackSpeed;
        
        public static float HealthRegen(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.HealthRegen, 0);
        public static void HealthRegen(this EntityStatsVariables stats, float staminaRegen) => stats[GenreEntityStatsVariableTypes.HealthRegen] = staminaRegen;
        
        public static float StaminaRegen(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.StaminaRegen, 0);
        public static void StaminaRegen(this EntityStatsVariables stats, float staminaRegen) => stats[GenreEntityStatsVariableTypes.StaminaRegen] = staminaRegen;
        
        public static float MovementSpeed(this EntityStatsVariables stats) => stats.GetValueOrDefault(GenreEntityStatsVariableTypes.MovementSpeed, 0);
        public static void MovementSpeed(this EntityStatsVariables stats, float movementSpeed) => stats[GenreEntityStatsVariableTypes.MovementSpeed] = movementSpeed;
    }
}