using OpenRpg.Entities.Classes;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Races;
using Xunit;

namespace OpenRpg.UnitTests.Core;

public class EntityTemplateVariableExtensionTests
{
    [Fact]
    public void should_use_same_race_data_instance_on_subsequent_access()
    {
        var vars = new EntityTemplateVariables();
        var race = vars.Race;
        race.TemplateId = 42;
        Assert.Equal(42, vars.Race.TemplateId);
    }

    [Fact]
    public void should_use_same_class_data_instance_on_subsequent_access()
    {
        var vars = new EntityTemplateVariables();
        var classData = vars.Class;
        classData.TemplateId = 99;
        Assert.Equal(99, vars.Class.TemplateId);
    }

    [Fact]
    public void should_use_same_multiclass_instance_on_subsequent_access()
    {
        var vars = new EntityTemplateVariables();
        var multiClass = vars.MultiClass;
        multiClass.Classes.Add(new ClassData());
        Assert.Single(vars.MultiClass.Classes);
    }
}
