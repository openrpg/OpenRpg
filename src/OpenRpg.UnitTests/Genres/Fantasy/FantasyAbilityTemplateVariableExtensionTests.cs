using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Genres.Fantasy.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyAbilityTemplateVariableExtensionTests
    {
        [Fact]
        public void should_get_default_mana_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.Equal(0, vars.ManaCost);
        }

        [Fact]
        public void should_apply_mana_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.ManaCost = 50;
            Assert.Equal(50, vars.ManaCost);
        }

        [Fact]
        public void should_identify_has_mana_cost()
        {
            var vars = new AbilityTemplateVariables();
            vars.ManaCost = 50;
            Assert.True(vars.HasManaCost());
        }

        [Fact]
        public void should_identify_not_have_mana_cost()
        {
            var vars = new AbilityTemplateVariables();
            Assert.False(vars.HasManaCost());
        }
    }
}
