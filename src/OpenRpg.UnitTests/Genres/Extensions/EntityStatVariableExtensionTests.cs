using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Extensions
{
    public class EntityStatVariableExtensionTests
    {
        [Fact]
        public void should_get_default_max_health()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0, stats.MaxHealth);
        }

        [Fact]
        public void should_apply_max_health()
        {
            var stats = new EntityStatsVariables();
            stats.MaxHealth = 100;
            Assert.Equal(100, stats.MaxHealth);
        }

        [Fact]
        public void should_get_default_max_stamina()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0, stats.MaxStamina);
        }

        [Fact]
        public void should_apply_max_stamina()
        {
            var stats = new EntityStatsVariables();
            stats.MaxStamina = 80;
            Assert.Equal(80, stats.MaxStamina);
        }

        [Fact]
        public void should_get_default_health_regen()
        {
            var stats = new EntityStatsVariables();
            Assert.Equal(0f, stats.HealthRegen);
        }

        [Fact]
        public void should_apply_health_regen()
        {
            var stats = new EntityStatsVariables();
            stats.HealthRegen = 2.5f;
            Assert.Equal(2.5f, stats.HealthRegen);
        }

        [Fact]
        public void should_apply_stamina_regen()
        {
            var stats = new EntityStatsVariables();
            stats.StaminaRegen = 1.5f;
            Assert.Equal(1.5f, stats.StaminaRegen);
        }

        [Fact]
        public void should_apply_health_regen_rate()
        {
            var stats = new EntityStatsVariables();
            stats.HealthRegenRate = 0.1f;
            Assert.Equal(0.1f, stats.HealthRegenRate);
        }

        [Fact]
        public void should_apply_stamina_regen_rate()
        {
            var stats = new EntityStatsVariables();
            stats.StaminaRegenRate = 0.2f;
            Assert.Equal(0.2f, stats.StaminaRegenRate);
        }

        [Fact]
        public void should_apply_damage_stat()
        {
            var stats = new EntityStatsVariables();
            stats.Damage = 50f;
            Assert.Equal(50f, stats.Damage);
        }

        [Fact]
        public void should_apply_defense_stat()
        {
            var stats = new EntityStatsVariables();
            stats.Defense = 25f;
            Assert.Equal(25f, stats.Defense);
        }

        [Fact]
        public void should_apply_critical_damage_chance()
        {
            var stats = new EntityStatsVariables();
            stats.CriticalDamageChance = 0.15f;
            Assert.Equal(0.15f, stats.CriticalDamageChance);
        }

        [Fact]
        public void should_apply_critical_damage_multiplier()
        {
            var stats = new EntityStatsVariables();
            stats.CriticalDamageMultiplier = 2f;
            Assert.Equal(2f, stats.CriticalDamageMultiplier);
        }

        [Fact]
        public void should_apply_cooldown_reduction()
        {
            var stats = new EntityStatsVariables();
            stats.CooldownReduction = 0.1f;
            Assert.Equal(0.1f, stats.CooldownReduction);
        }

        [Fact]
        public void should_apply_attack_size()
        {
            var stats = new EntityStatsVariables();
            stats.AttackSize = 3f;
            Assert.Equal(3f, stats.AttackSize);
        }

        [Fact]
        public void should_apply_attack_range()
        {
            var stats = new EntityStatsVariables();
            stats.AttackRange = 10f;
            Assert.Equal(10f, stats.AttackRange);
        }

        [Fact]
        public void should_apply_movement_speed()
        {
            var stats = new EntityStatsVariables();
            stats.MovementSpeed = 5f;
            Assert.Equal(5f, stats.MovementSpeed);
        }
    }
}
