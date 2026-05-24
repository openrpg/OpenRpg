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
        
        [Fact]
        public void should_correctly_add_health()
        {
            var expectedHealth = 123;
            var entityState = new EntityStateVariables();
            entityState.Health = 100;
            entityState.AddHealth(23);

            var actualHealth = entityState.Health;
            Assert.Equal(expectedHealth, actualHealth);
        }
        
        [Fact]
        public void should_correctly_add_health_with_maxhealth()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 50;
            entityState.AddHealth(30, 100);

            var actualHealth = entityState.Health;
            Assert.Equal(80, actualHealth);
        }

        [Fact]
        public void should_correctly_add_health_with_maxhealth_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 85;
            entityState.AddHealth(30, 100);

            var actualHealth = entityState.Health;
            Assert.Equal(100, actualHealth);
        }

        [Fact]
        public void should_correctly_deduct_health_with_maxhealth()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 80;
            entityState.DeductHealth(30, 100);

            var actualHealth = entityState.Health;
            Assert.Equal(50, actualHealth);
        }

        [Fact]
        public void should_correctly_deduct_health_with_maxhealth_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 20;
            entityState.DeductHealth(50, 100);

            var actualHealth = entityState.Health;
            Assert.Equal(0, actualHealth);
        }

        [Fact]
        public void should_identify_is_dead()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 0;
            Assert.True(entityState.IsDead);
        }

        [Fact]
        public void should_identify_is_not_dead()
        {
            var entityState = new EntityStateVariables();
            entityState.Health = 1;
            Assert.False(entityState.IsDead);
        }

        [Fact]
        public void should_restore_life()
        {
            var entityState = new EntityStateVariables();
            entityState.RestoreLife(50);
            Assert.Equal(50, entityState.Health);
        }

        [Fact]
        public void should_restore_life_with_maxhealth()
        {
            var entityState = new EntityStateVariables();
            entityState.RestoreLife(80, 100);
            Assert.Equal(80, entityState.Health);
        }

        [Fact]
        public void should_restore_life_with_maxhealth_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.RestoreLife(120, 100);
            Assert.Equal(100, entityState.Health);
        }

        [Fact]
        public void should_restore_life_with_minimum_to_one()
        {
            var entityState = new EntityStateVariables();
            entityState.RestoreLife(0);
            Assert.Equal(1, entityState.Health);
        }

        [Fact]
        public void should_apply_stamina_changes()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 75;
            Assert.Equal(75, entityState.Stamina);
        }

        [Fact]
        public void should_add_stamina()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 100;
            entityState.AddStamina(23);
            Assert.Equal(123, entityState.Stamina);
        }

        [Fact]
        public void should_deduct_stamina()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 123;
            entityState.DeductStamina(23);
            Assert.Equal(100, entityState.Stamina);
        }

        [Fact]
        public void should_add_stamina_with_maxstamina()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 50;
            entityState.AddStamina(30, 100);
            Assert.Equal(80, entityState.Stamina);
        }

        [Fact]
        public void should_add_stamina_with_maxstamina_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 85;
            entityState.AddStamina(30, 100);
            Assert.Equal(100, entityState.Stamina);
        }

        [Fact]
        public void should_deduct_stamina_with_maxstamina()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 80;
            entityState.DeductStamina(30, 100);
            Assert.Equal(50, entityState.Stamina);
        }

        [Fact]
        public void should_deduct_stamina_with_maxstamina_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Stamina = 20;
            entityState.DeductStamina(50, 100);
            Assert.Equal(0, entityState.Stamina);
        }

        [Fact]
        public void should_apply_mana_changes()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 75;
            Assert.Equal(75f, entityState.Mana);
        }

        [Fact]
        public void should_add_mana()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 100;
            entityState.AddMana(23);
            Assert.Equal(123f, entityState.Mana);
        }

        [Fact]
        public void should_deduct_mana()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 123;
            entityState.DeductMana(23);
            Assert.Equal(100f, entityState.Mana);
        }

        [Fact]
        public void should_add_mana_with_maxmana()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 50;
            entityState.AddMana(30, 100);
            Assert.Equal(80f, entityState.Mana);
        }

        [Fact]
        public void should_add_mana_with_maxmana_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 85;
            entityState.AddMana(30, 100);
            Assert.Equal(100f, entityState.Mana);
        }

        [Fact]
        public void should_deduct_mana_with_maxmana()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 80;
            entityState.DeductMana(30, 100);
            Assert.Equal(50f, entityState.Mana);
        }

        [Fact]
        public void should_deduct_mana_with_maxmana_clamped()
        {
            var entityState = new EntityStateVariables();
            entityState.Mana = 20;
            entityState.DeductMana(50, 100);
            Assert.Equal(0f, entityState.Mana);
        }

        [Fact]
        public void should_correctly_deduct_health()
        {
            var expectedHealth = 100;
            var entityState = new EntityStateVariables();
            entityState.Health = 123;
            entityState.DeductHealth(23);

            var actualHealth = entityState.Health;
            Assert.Equal(expectedHealth, actualHealth);
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