using System.Collections.Generic;
using OpenRpg.Core.Variables;
using Xunit;

namespace OpenRpg.UnitTests.Framework;

public class DefaultKeyedVariablesTests
{
    [Fact]
    public void should_correctly_raise_add_event_on_adding_variables()
    {
        var expectedArgs = new VariableEventArgs<int, int>(1, default, 10);
        VariableEventArgs<int, int> actualArgs = null; 
        
        var variables = new DefaultKeyedVariables<int, int>(1);
        variables.OnAdded += (sender, args) => actualArgs = args;
        
        variables.AddVariable(expectedArgs.VariableType, expectedArgs.NewValue);
        
        Assert.NotNull(actualArgs);
        Assert.Equal(expectedArgs.VariableType, actualArgs.VariableType);
        Assert.Equal(expectedArgs.OldValue, actualArgs.OldValue);
        Assert.Equal(expectedArgs.NewValue, actualArgs.NewValue);
        
        Assert.Equal(expectedArgs.NewValue, variables.InternalVariables[expectedArgs.VariableType]);
    }
    
    [Fact]
    public void should_correctly_raise_change_event_on_updating_variables()
    {
        var expectedArgs = new VariableEventArgs<int, int>(1, 10, 100);
        VariableEventArgs<int, int> actualArgs = null;
        
        var variables = new DefaultKeyedVariables<int, int>(1, new Dictionary<int, int> { {1, 10} });
        variables.OnChanged += (sender, args) => actualArgs = args;
        
        variables[expectedArgs.VariableType] = expectedArgs.NewValue;
        
        Assert.NotNull(actualArgs);
        Assert.Equal(expectedArgs.VariableType, actualArgs.VariableType);
        Assert.Equal(expectedArgs.OldValue, actualArgs.OldValue);
        Assert.Equal(expectedArgs.NewValue, actualArgs.NewValue);
        
        Assert.Equal(expectedArgs.NewValue, variables.InternalVariables[expectedArgs.VariableType]);
    }
    
    [Fact]
    public void should_correctly_raise_remove_event_on_removing_variables()
    {
        var expectedArgs = new VariableEventArgs<int, int>(1, 10, default);
        VariableEventArgs<int, int> actualArgs = null; 
        
        var variables = new DefaultKeyedVariables<int, int>(1, new Dictionary<int, int> { {1, 10} });
        variables.OnRemoved += (sender, args) => actualArgs = args;
        
        variables.Remove(expectedArgs.VariableType);
        
        Assert.NotNull(actualArgs);
        Assert.Equal(expectedArgs.VariableType, actualArgs.VariableType);
        Assert.Equal(expectedArgs.OldValue, actualArgs.OldValue);
        Assert.Equal(expectedArgs.NewValue, actualArgs.NewValue);
        
        Assert.False(variables.InternalVariables.ContainsKey(expectedArgs.VariableType));
    }
    
    [Fact]
    public void should_do_insert_on_key_update_if_key_doesnt_exist()
    {
        var expectedAddArgs = new VariableEventArgs<int, int>(1, default, 10);
        var expectedChangeArgs = new VariableEventArgs<int, int>(1, 10, 100);
        VariableEventArgs<int, int> actualAddArgs = null; 
        VariableEventArgs<int, int> actualChangedArgs = null; 
        
        var variables = new DefaultKeyedVariables<int, int>(1);
        variables.OnAdded += (sender, args) => actualAddArgs = args;
        variables.OnChanged += (sender, args) => actualChangedArgs = args;

        variables[expectedAddArgs.VariableType] = expectedAddArgs.NewValue;
        variables[expectedChangeArgs.VariableType] = expectedChangeArgs.NewValue;
        
        Assert.NotNull(actualAddArgs);
        Assert.Equal(expectedAddArgs.VariableType, actualAddArgs.VariableType);
        Assert.Equal(expectedAddArgs.OldValue, actualAddArgs.OldValue);
        Assert.Equal(expectedAddArgs.NewValue, actualAddArgs.NewValue);     
        
        Assert.NotNull(actualChangedArgs);
        Assert.Equal(expectedChangeArgs.VariableType, actualChangedArgs.VariableType);
        Assert.Equal(expectedChangeArgs.OldValue, actualChangedArgs.OldValue);
        Assert.Equal(expectedChangeArgs.NewValue, actualChangedArgs.NewValue);
        
        Assert.Equal(expectedChangeArgs.NewValue, variables.InternalVariables[expectedChangeArgs.VariableType]);
    }
}