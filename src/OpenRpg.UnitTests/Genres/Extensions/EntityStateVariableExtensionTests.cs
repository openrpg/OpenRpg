using System;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Entities.State.Variables;
using OpenRpg.Genres.Extensions;
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
            entityState.Health(expectedHealth);

            var actualHealth = entityState.Health();
            Assert.Equal(expectedHealth, actualHealth);
        }
        
        [Fact]
        public void should_correctly_add_health()
        {
            var expectedHealth = 123;
            var entityState = new EntityStateVariables();
            entityState.Health(100);
            entityState.AddHealth(23);

            var actualHealth = entityState.Health();
            Assert.Equal(expectedHealth, actualHealth);
        }
        
        [Fact]
        public void should_correctly_deduct_health()
        {
            var expectedHealth = 100;
            var entityState = new EntityStateVariables();
            entityState.Health(123);
            entityState.DeductHealth(23);

            var actualHealth = entityState.Health();
            Assert.Equal(expectedHealth, actualHealth);
        }
        
        [Fact]
        public void should_correctly_apply_damage()
        {
            var expectedHealth = 50;
            var entityState = new EntityStateVariables();
            entityState.Health(100);

            var processedAttack = new ProcessedAttack(new[] { new Damage(GenreDamageTypes.Damage, 50) }, Array.Empty<Damage>());
            entityState.ApplyDamageToTarget(processedAttack);
            
            var actualHealth = entityState.Health();
            Assert.Equal(expectedHealth, actualHealth);
        }
    }
}