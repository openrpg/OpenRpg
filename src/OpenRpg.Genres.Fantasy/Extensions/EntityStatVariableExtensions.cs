using System.Collections.Generic;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EntityStatVariableExtensions
    {
        extension(EntityStatsVariables stats)
        {
            public int Strength
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Strength);
                set => stats[FantasyEntityStatsVariableTypes.Strength] = value;
            }
            
            public int Dexterity
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Dexterity);
                set => stats[FantasyEntityStatsVariableTypes.Dexterity] = value;
            }
            
            public int Constitution
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Constitution);
                set => stats[FantasyEntityStatsVariableTypes.Constitution] = value;
            }
            
            public int Intelligence
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Intelligence);
                set => stats[FantasyEntityStatsVariableTypes.Intelligence] = value;
            }
            
            public int Wisdom
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Wisdom);
                set => stats[FantasyEntityStatsVariableTypes.Wisdom] = value;
            }
            
            public int Charisma
            {
                get => (int)stats.Get(FantasyEntityStatsVariableTypes.Charisma);
                set => stats[FantasyEntityStatsVariableTypes.Charisma] = value;
            }
        }

        extension(EntityStatsVariables stats)
        {
            public float MaxMana
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.MaxMana);
                set => stats[FantasyEntityStatsVariableTypes.MaxMana] = value;
            }
            
            public float ManaRegen
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.ManaRegen);
                set => stats[FantasyEntityStatsVariableTypes.ManaRegen] = value;
            }
            
            public float ManaRegenRate
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.ManaRegenRate);
                set => stats[FantasyEntityStatsVariableTypes.ManaRegenRate] = value;
            }
        }
        
        extension(EntityStatsVariables stats)
        {
            public float IceDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.IceDamage);
                set => stats[FantasyEntityStatsVariableTypes.IceDamage] = value;
            }
            
            public float FireDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.FireDamage);
                set => stats[FantasyEntityStatsVariableTypes.FireDamage] = value;
            }
            
            public float WindDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.WindDamage);
                set => stats[FantasyEntityStatsVariableTypes.WindDamage] = value;
            }
            
            public float EarthDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.EarthDamage);
                set => stats[FantasyEntityStatsVariableTypes.EarthDamage] = value;
            }
            
            public float LightDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.LightDamage);
                set => stats[FantasyEntityStatsVariableTypes.LightDamage] = value;
            }
            
            public float DarkDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.DarkDamage);
                set => stats[FantasyEntityStatsVariableTypes.DarkDamage] = value;
            }
            
            public float SlashingDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.SlashingDamage);
                set => stats[FantasyEntityStatsVariableTypes.SlashingDamage] = value;
            }
            
            public float BluntDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.BluntDamage);
                set => stats[FantasyEntityStatsVariableTypes.BluntDamage] = value;
            }
            
            public float PiercingDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.PiercingDamage);
                set => stats[FantasyEntityStatsVariableTypes.PiercingDamage] = value;
            }
            
            public float UnarmedDamage
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDamage);
                set => stats[FantasyEntityStatsVariableTypes.UnarmedDamage] = value;
            }
        }
        
        extension(EntityStatsVariables stats)
        {
            public float IceDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.IceDefense);
                set => stats[FantasyEntityStatsVariableTypes.IceDefense] = value;
            }
            
            public float FireDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.FireDefense);
                set => stats[FantasyEntityStatsVariableTypes.FireDefense] = value;
            }
            
            public float WindDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.WindDefense);
                set => stats[FantasyEntityStatsVariableTypes.WindDefense] = value;
            }
            
            public float EarthDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.EarthDefense);
                set => stats[FantasyEntityStatsVariableTypes.EarthDefense] = value;
            }
            
            public float LightDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.LightDefense);
                set => stats[FantasyEntityStatsVariableTypes.LightDefense] = value;
            }
            
            public float DarkDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.DarkDefense);
                set => stats[FantasyEntityStatsVariableTypes.DarkDefense] = value;
            }
            
            public float SlashingDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.SlashingDefense);
                set => stats[FantasyEntityStatsVariableTypes.SlashingDefense] = value;
            }
            
            public float BluntDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.BluntDefense);
                set => stats[FantasyEntityStatsVariableTypes.BluntDefense] = value;
            }
            
            public float PiercingDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.PiercingDefense);
                set => stats[FantasyEntityStatsVariableTypes.PiercingDefense] = value;
            }
            
            public float UnarmedDefense
            {
                get => stats.Get(FantasyEntityStatsVariableTypes.UnarmedDefense);
                set => stats[FantasyEntityStatsVariableTypes.UnarmedDefense] = value;
            }
        }
    }
}