using OpenRpg.Entities.Extensions;
using OpenRpg.Quests.Variables;
using Xunit;

namespace OpenRpg.UnitTests.Core;

public class TemplateVariableExtensionsTests
{
    [Fact]
    public void should_use_same_procedural_effects_instance_on_subsequent_access()
    {
        var vars = new QuestVariables();
        var first = vars.ProceduralEffects;
        var second = vars.ProceduralEffects;
        Assert.Same(first, second);
    }

    [Fact]
    public void should_use_same_effects_instance_on_subsequent_access()
    {
        var vars = new QuestVariables();
        var first = vars.Effects;
        var second = vars.Effects;
        Assert.Same(first, second);
    }

    [Fact]
    public void should_use_same_requirements_instance_on_subsequent_access()
    {
        var vars = new QuestVariables();
        var first = vars.Requirements;
        var second = vars.Requirements;
        Assert.Same(first, second);
    }
}
