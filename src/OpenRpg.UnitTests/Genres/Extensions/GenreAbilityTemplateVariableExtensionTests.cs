using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Genres.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Extensions
{
    public class GenreAbilityTemplateVariableExtensionTests
    {
        [Fact]
        public void should_get_default_health_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.Equal(0, vars.HealthCost);
        }

        [Fact]
        public void should_apply_health_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.HealthCost = 50;
            Assert.Equal(50, vars.HealthCost);
        }

        [Fact]
        public void should_identify_has_health_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.HealthCost = 50;
            Assert.True(vars.HasHealthCost());
        }

        [Fact]
        public void should_identify_not_have_health_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.False(vars.HasHealthCost());
        }

        [Fact]
        public void should_get_default_stamina_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.Equal(0, vars.StaminaCost);
        }

        [Fact]
        public void should_apply_stamina_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.StaminaCost = 30;
            Assert.Equal(30, vars.StaminaCost);
        }

        [Fact]
        public void should_identify_has_stamina_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.StaminaCost = 30;
            Assert.True(vars.HasStaminaCost());
        }

        [Fact]
        public void should_identify_not_have_stamina_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.False(vars.HasStaminaCost());
        }
    }
}
