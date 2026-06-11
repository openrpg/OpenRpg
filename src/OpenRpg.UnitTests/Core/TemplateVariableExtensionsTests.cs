using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
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

    [Fact]
    public void should_normalize_array_to_list_when_setting_effects()
    {
        var vars = new QuestVariables();
        var effects = new[]
        {
            new StaticEffect { EffectType = 1, Potency = 10 },
            new StaticEffect { EffectType = 2, Potency = 20 }
        };

        vars.Effects = effects;

        var result = vars.Effects;
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.IsType<List<IEffect>>(result);
    }

    [Fact]
    public void should_normalize_array_to_list_when_setting_requirements()
    {
        var vars = new QuestVariables();
        var requirements = new[]
        {
            new Requirement { RequirementType = 1, Association = new OpenRpg.Core.Associations.Association(0, 5) }
        };

        vars.Requirements = requirements;

        var result = vars.Requirements;
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.IsType<List<Requirement>>(result);
    }

    [Fact]
    public void should_preserve_list_reference_when_setting_effects_with_list()
    {
        var vars = new QuestVariables();
        var effects = new List<IEffect>
        {
            new StaticEffect { EffectType = 1, Potency = 10 }
        };

        vars.Effects = effects;

        Assert.Same(effects, vars.Effects);
    }

    [Fact]
    public void should_store_empty_list_when_setting_effects_with_null()
    {
        var vars = new QuestVariables();
        vars.Effects = null;

        var result = vars.Effects;
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void should_return_same_instance_after_setting_array_for_effects()
    {
        var vars = new QuestVariables();
        vars.Effects = new[] { new StaticEffect { EffectType = 1, Potency = 10 } };

        var first = vars.Effects;
        var second = vars.Effects;
        Assert.Same(first, second);
    }

    [Fact]
    public void should_return_same_instance_after_setting_array_for_requirements()
    {
        var vars = new QuestVariables();
        vars.Requirements = new[] { new Requirement { RequirementType = 1 } };

        var first = vars.Requirements;
        var second = vars.Requirements;
        Assert.Same(first, second);
    }

    [Fact]
    public void should_preserve_effects_content_after_normalizing_array()
    {
        var vars = new QuestVariables();
        var effect1 = new StaticEffect { EffectType = 100, Potency = 15.0f };
        var effect2 = new StaticEffect { EffectType = 200, Potency = 25.0f };

        vars.Effects = new IEffect[] { effect1, effect2 };

        var result = vars.Effects.ToArray();
        Assert.Equal(2, result.Length);
        Assert.Equal(100, result[0].EffectType);
        Assert.Equal(15.0f, ((StaticEffect)result[0]).Potency);
        Assert.Equal(200, result[1].EffectType);
        Assert.Equal(25.0f, ((StaticEffect)result[1]).Potency);
    }
}
