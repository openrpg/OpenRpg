using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Core
{
    public class ClassVariableExtensionTests
    {
        [Fact]
        public void should_get_default_experience()
        {
            var vars = new ClassVariables();
            Assert.Equal(0, vars.Experience);
        }

        [Fact]
        public void should_apply_experience()
        {
            var vars = new ClassVariables();
            vars.Experience = 1000;
            Assert.Equal(1000, vars.Experience);
        }

        [Fact]
        public void should_add_experience()
        {
            var vars = new ClassVariables();
            vars.Experience = 500;
            vars.AddExperience(250);
            Assert.Equal(750, vars.Experience);
        }
    }
}
