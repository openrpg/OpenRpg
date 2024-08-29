using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Races;
using OpenRpg.Core.Races.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Core;

public class EntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_gender_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasGender());
        
        var dummyGender = (byte)1;
        entityVars.Gender(dummyGender);
        Assert.True(entityVars.HasGender());
        Assert.Equal(entityVars.Gender(), dummyGender);
    }
    
    [Fact]
    public void should_correctly_handle_race_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasRace());
        
        var dummyRace = new Race();
        entityVars.Race(dummyRace);
        Assert.True(entityVars.HasRace());
        Assert.Equal(entityVars.Race(), dummyRace);
    }
    
    [Fact]
    public void should_correctly_handle_class_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasClass());
        
        var dummyClass = new Class();
        entityVars.Class(dummyClass);
        Assert.True(entityVars.HasClass());
        Assert.Equal(entityVars.Class(), dummyClass);
    }
    
    [Fact]
    public void should_correctly_handle_multiclass_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasMultiClass());
        
        var dummyMultiClass = new MultiClasses();
        entityVars.MultiClass(dummyMultiClass);
        Assert.True(entityVars.HasMultiClass());
        Assert.Equal(entityVars.MultiClass(), dummyMultiClass);
    }
}