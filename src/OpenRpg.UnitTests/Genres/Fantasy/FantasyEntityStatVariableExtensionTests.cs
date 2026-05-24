using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyEntityStatVariableExtensionTests
    {
        [Fact]
        public void should_get_default_stats()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0, stats.Strength);
            Assert.Equal(0, stats.Dexterity);
            Assert.Equal(0, stats.Constitution);
            Assert.Equal(0, stats.Intelligence);
            Assert.Equal(0, stats.Wisdom);
            Assert.Equal(0, stats.Charisma);
        }

        [Fact]
        public void should_apply_stats()
        {
            var stats = new EntityStatsVariables();
            stats.Strength = 10;
            stats.Dexterity = 12;
            stats.Constitution = 14;
            stats.Intelligence = 16;
            stats.Wisdom = 18;
            stats.Charisma = 20;
            Assert.Equal(10, stats.Strength);
            Assert.Equal(12, stats.Dexterity);
            Assert.Equal(14, stats.Constitution);
            Assert.Equal(16, stats.Intelligence);
            Assert.Equal(18, stats.Wisdom);
            Assert.Equal(20, stats.Charisma);
        }

        [Fact]
        public void should_get_default_mana_stats()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0f, stats.MaxMana);
            Assert.Equal(0f, stats.ManaRegen);
            Assert.Equal(0f, stats.ManaRegenRate);
        }

        [Fact]
        public void should_apply_mana_stats()
        {
            var stats = new EntityStatsVariables();
            stats.MaxMana = 100f;
            stats.ManaRegen = 5f;
            stats.ManaRegenRate = 0.5f;
            Assert.Equal(100f, stats.MaxMana);
            Assert.Equal(5f, stats.ManaRegen);
            Assert.Equal(0.5f, stats.ManaRegenRate);
        }

        [Fact]
        public void should_get_default_damage_types()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0f, stats.IceDamage);
            Assert.Equal(0f, stats.FireDamage);
            Assert.Equal(0f, stats.WindDamage);
            Assert.Equal(0f, stats.EarthDamage);
            Assert.Equal(0f, stats.LightDamage);
            Assert.Equal(0f, stats.DarkDamage);
            Assert.Equal(0f, stats.SlashingDamage);
            Assert.Equal(0f, stats.BluntDamage);
            Assert.Equal(0f, stats.PiercingDamage);
            Assert.Equal(0f, stats.UnarmedDamage);
        }

        [Fact]
        public void should_apply_damage_types()
        {
            var stats = new EntityStatsVariables();
            stats.IceDamage = 10f;
            stats.FireDamage = 20f;
            stats.WindDamage = 30f;
            stats.EarthDamage = 40f;
            stats.LightDamage = 50f;
            stats.DarkDamage = 60f;
            stats.SlashingDamage = 70f;
            stats.BluntDamage = 80f;
            stats.PiercingDamage = 90f;
            stats.UnarmedDamage = 100f;
            Assert.Equal(10f, stats.IceDamage);
            Assert.Equal(20f, stats.FireDamage);
            Assert.Equal(30f, stats.WindDamage);
            Assert.Equal(40f, stats.EarthDamage);
            Assert.Equal(50f, stats.LightDamage);
            Assert.Equal(60f, stats.DarkDamage);
            Assert.Equal(70f, stats.SlashingDamage);
            Assert.Equal(80f, stats.BluntDamage);
            Assert.Equal(90f, stats.PiercingDamage);
            Assert.Equal(100f, stats.UnarmedDamage);
        }

        [Fact]
        public void should_get_default_defense_types()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0f, stats.IceDefense);
            Assert.Equal(0f, stats.FireDefense);
            Assert.Equal(0f, stats.WindDefense);
            Assert.Equal(0f, stats.EarthDefense);
            Assert.Equal(0f, stats.LightDefense);
            Assert.Equal(0f, stats.DarkDefense);
            Assert.Equal(0f, stats.SlashingDefense);
            Assert.Equal(0f, stats.BluntDefense);
            Assert.Equal(0f, stats.PiercingDefense);
            Assert.Equal(0f, stats.UnarmedDefense);
        }

        [Fact]
        public void should_apply_defense_types()
        {
            var stats = new EntityStatsVariables();
            stats.IceDefense = 5f;
            stats.FireDefense = 10f;
            stats.WindDefense = 15f;
            stats.EarthDefense = 20f;
            stats.LightDefense = 25f;
            stats.DarkDefense = 30f;
            stats.SlashingDefense = 35f;
            stats.BluntDefense = 40f;
            stats.PiercingDefense = 45f;
            stats.UnarmedDefense = 50f;
            Assert.Equal(5f, stats.IceDefense);
            Assert.Equal(10f, stats.FireDefense);
            Assert.Equal(15f, stats.WindDefense);
            Assert.Equal(20f, stats.EarthDefense);
            Assert.Equal(25f, stats.LightDefense);
            Assert.Equal(30f, stats.DarkDefense);
            Assert.Equal(35f, stats.SlashingDefense);
            Assert.Equal(40f, stats.BluntDefense);
            Assert.Equal(45f, stats.PiercingDefense);
            Assert.Equal(50f, stats.UnarmedDefense);
        }
    }
}
