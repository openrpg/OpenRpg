using System;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Types;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Extensions
{
    public class EntityStateVariableExtensionTests
    {
        [Fact]
        public void should_correctly_apply_health_changes()
        {
            var expectedHealth = 123;
            var entityState = new EntityStateVariables();
            entityState.Health = expectedHealth;

            var actualHealth = entityState.Health;
            Assert.Equal(expectedHealth, actualHealth);
        }

        [Theory]
        [InlineData(100, 23, null, 123)]
        [InlineData(50, 30, 100, 80)]
        [InlineData(85, 30, 100, 100)]
        public void should_add_health(int initial, int change, int? maxHealth, int expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Health = initial;
            entityState.AddHealth(change, maxHealth);
            Assert.Equal(expected, entityState.Health);
        }

        [Theory]
        [InlineData(123, 23, null, 100)]
        [InlineData(80, 30, 100, 50)]
        [InlineData(20, 50, 100, 0)]
        public void should_deduct_health(int initial, int change, int? maxHealth, int expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Health = initial;
            entityState.DeductHealth(change, maxHealth);
            Assert.Equal(expected, entityState.Health);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void should_identify_if_dead(int health, bool expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Health = health;
            Assert.Equal(expected, entityState.IsDead);
        }

        [Theory]
        [InlineData(50, null, 50)]
        [InlineData(80, 100, 80)]
        [InlineData(120, 100, 100)]
        [InlineData(0, null, 1)]
        public void should_restore_life(int amount, int? maxHealth, int expected)
        {
            var entityState = new EntityStateVariables();
            entityState.RestoreLife(amount, maxHealth);
            Assert.Equal(expected, entityState.Health);
        }

        [Fact]
        public void should_apply_stamina_changes()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 75;
            Assert.Equal(75, entityState.Stamina);
        }

        [Theory]
        [InlineData(100, 23, null, 123)]
        [InlineData(50, 30, 100, 80)]
        [InlineData(85, 30, 100, 100)]
        public void should_add_stamina(int initial, int change, int? maxStamina, int expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = initial;
            entityState.AddStamina(change, maxStamina);
            Assert.Equal(expected, entityState.Stamina);
        }

        [Theory]
        [InlineData(123, 23, null, 100)]
        [InlineData(80, 30, 100, 50)]
        [InlineData(20, 50, 100, 0)]
        public void should_deduct_stamina(int initial, int change, int? maxStamina, int expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = initial;
            entityState.DeductStamina(change, maxStamina);
            Assert.Equal(expected, entityState.Stamina);
        }

        [Fact]
        public void should_apply_mana_changes()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 75;
            Assert.Equal(75f, entityState.Mana);
        }

        [Theory]
        [InlineData(100, 23, null, 123f)]
        [InlineData(50, 30, 100, 80f)]
        [InlineData(85, 30, 100, 100f)]
        public void should_add_mana(int initial, int change, int? maxMana, float expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = initial;
            entityState.AddMana(change, maxMana);
            Assert.Equal(expected, entityState.Mana);
        }

        [Theory]
        [InlineData(123, 23, null, 100f)]
        [InlineData(80, 30, 100, 50f)]
        [InlineData(20, 50, 100, 0f)]
        public void should_deduct_mana(int initial, int change, int? maxMana, float expected)
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = initial;
            entityState.DeductMana(change, maxMana);
            Assert.Equal(expected, entityState.Mana);
        }

        [Fact]
        public void should_correctly_apply_damage()
        {
            var expectedHealth = 50;
            var entityState = new EntityStateVariables();
            entityState.Health = 100;

            var processedAttack = new ProcessedAttack(new[] { new Damage(GenreDamageTypes.Damage, 50) }, Array.Empty<Damage>());
            entityState.ApplyDamageToTarget(processedAttack);

            var actualHealth = entityState.Health;
            Assert.Equal(expectedHealth, actualHealth);
        }
    }
}
