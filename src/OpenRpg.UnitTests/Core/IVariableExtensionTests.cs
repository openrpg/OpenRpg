using System.Linq;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Inventory;
using Xunit;

namespace OpenRpg.UnitTests.Core;

public class IVariableExtensionTests
{
    [Fact]
    public void should_get_default_int_when_key_doesnt_exist_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        var result = vars.GetInt(0);
        Assert.Equal(default(int), result);
    }
    
    [Fact]
    public void should_get_default_int_when_value_is_null_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        vars[0] = null!;
        var result = vars.GetInt(0);
        Assert.Equal(default(int), result);
    }
    
    [Fact]
    public void should_get_specific_int_when_value_is_set_for_object_variables()
    {
        var expected = 22;
        var vars = new DefaultVariables<object>(0);
        vars[0] = expected;
        var result = vars.GetInt(0);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void should_get_default_float_when_key_doesnt_exist_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        var result = vars.GetFloat(0);
        Assert.Equal(default(float), result);
    }
    
    [Fact]
    public void should_get_default_float_when_value_is_null_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        vars[0] = null!;
        var result = vars.GetFloat(0);
        Assert.Equal(default(float), result);
    }
    
    [Fact]
    public void should_get_specific_float_when_value_is_set_for_object_variables()
    {
        var expected = 22.5f;
        var vars = new DefaultVariables<object>(0);
        vars[0] = expected;
        var result = vars.GetFloat(0);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void should_get_default_bool_when_key_doesnt_exist_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        var result = vars.GetBool(0);
        Assert.Equal(default(bool), result);
    }
    
    [Fact]
    public void should_get_default_bool_when_value_is_null_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        vars[0] = null!;
        var result = vars.GetBool(0);
        Assert.Equal(default(bool), result);
    }
    
    [Fact]
    public void should_get_specific_bool_when_value_is_set_for_object_variables()
    {
        var expected = true;
        var vars = new DefaultVariables<object>(0);
        vars[0] = expected;
        var result = vars.GetBool(0);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void should_get_default_byte_when_key_doesnt_exist_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        var result = vars.GetByte(0);
        Assert.Equal(default(byte), result);
    }
    
    [Fact]
    public void should_get_default_byte_when_value_is_null_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        vars[0] = null!;
        var result = vars.GetByte(0);
        Assert.Equal(default(byte), result);
    }
    
    [Fact]
    public void should_get_specific_byte_when_value_is_set_for_object_variables()
    {
        var expected = (byte)22;
        var vars = new DefaultVariables<object>(0);
        vars[0] = expected;
        var result = vars.GetByte(0);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void should_get_null_when_key_doesnt_exist_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        var result = vars.GetAs<IInventory>(0);
        Assert.Equal(null, result);
    }
    
    [Fact]
    public void should_get_null_when_value_is_null_for_object_variables()
    {
        var vars = new DefaultVariables<object>(0);
        vars[0] = null!;
        var result = vars.GetAs<IInventory>(0);
        Assert.Equal(null, result);
    }
    
    [Fact]
    public void should_get_specific_object_when_instance_is_set_for_object_variables()
    {
        var expected = new DefaultInventory();
        var vars = new DefaultVariables<object>(0);
        vars[0] = expected;
        var result = vars.GetAs<IInventory>(0);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void should_set_value_if_no_value_exists_when_adding_value_for_int_variables()
    {
        var expected = 22;
        var vars = new DefaultVariables<int>(0);
        vars.AddValue(0, expected);
        
        Assert.True(vars.ContainsKey(0));
        Assert.Equal(expected, vars[0]);
    }
    
    [Fact]
    public void should_add_value_to_existing_when_adding_value_for_int_variables()
    {
        var starting = 22;
        var addition = 10;
        var expected = 32;
        
        var vars = new DefaultVariables<int>(0);
        vars[0] = starting;
        var actualValue = vars.AddValue(0, addition);
        
        Assert.True(vars.ContainsKey(0));
        Assert.Equal(expected, actualValue);
        Assert.Equal(expected, vars[0]);
    }
    
    [Fact]
    public void should_deduct_value_to_existing_when_adding_value_for_int_variables()
    {
        var starting = 22;
        var deduction = -10;
        var expected = 12;
        
        var vars = new DefaultVariables<int>(0);
        vars[0] = starting;
        var actualValue = vars.AddValue(0, deduction);
        
        Assert.True(vars.ContainsKey(0));
        Assert.Equal(expected, actualValue);
        Assert.Equal(expected, vars[0]);
    }
    
    [Theory]
    [InlineData(0, 10, 5, 5)]
    [InlineData(0, 10, 10, 10)]
    [InlineData(0, 10, 11, 10)]
    [InlineData(0, 10, -10, 0)]
    public void should_set_value_within_min_max_threshold(int min, int max, int valueToApply, int expected)
    {
        var vars = new DefaultVariables<int>(0);
        var actualValue = vars.AddValue(0, valueToApply, min, max);
        
        Assert.True(vars.ContainsKey(0));
        Assert.Equal(expected, actualValue);
        Assert.Equal(expected, vars[0]);
    }
    
        
    [Theory]
    [InlineData(2, 0, 10, 5, 7)]
    [InlineData(2, 0, 10, 8, 10)]
    [InlineData(5, 0, 10, 11, 10)]
    [InlineData(5, 0, 10, -3, 2)]
    public void should_set_value_within_min_max_threshold_with_starting_value(int start, int min, int max, int valueToApply, int expected)
    {
        var vars = new DefaultVariables<int>(0);
        vars[0] = start;
        var actualValue = vars.AddValue(0, valueToApply, min, max);
        
        Assert.True(vars.ContainsKey(0));
        Assert.Equal(expected, actualValue);
        Assert.Equal(expected, vars[0]);
    }
}