using OpenRpg.Core.Utils;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Ranges;

public class RangeTests
{
    [Fact]
    public void should_correctly_convert_from_int_range_to_float_range()
    {
        var expectedRange = new RangeF(22, 33);
        var range = new Range(22, 33);
        RangeF actualRange = range;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_add_from_two_ranges()
    {
        var expectedRange = new Range(50, 100);
        var rangeA = new Range(25, 50);
        var rangeB = new Range(25, 50);
        var actualRange = rangeA + rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_add_from_number()
    {
        var expectedRange = new Range(50, 75);
        var rangeA = new Range(25, 50);
        var number = 25;
        var actualRange = rangeA + number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_subtract_from_two_ranges()
    {
        var expectedRange = new Range(0, 25);
        var rangeA = new Range(25, 50);
        var rangeB = new Range(25, 25);
        var actualRange = rangeA - rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_subtract_from_number()
    {
        var expectedRange = new Range(0, 25);
        var rangeA = new Range(25, 50);
        var number = 25;
        var actualRange = rangeA - number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_multiply_from_two_ranges()
    {
        var expectedRange = new Range(50, 500);
        var rangeA = new Range(5, 50);
        var rangeB = new Range(10, 10);
        var actualRange = rangeA * rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_multiply_from_number()
    {
        var expectedRange = new Range(250, 500);
        var rangeA = new Range(25, 50);
        var number = 10;
        var actualRange = rangeA * number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
        
    [Fact]
    public void should_correctly_divide_from_two_ranges()
    {
        var expectedRange = new Range(10, 5);
        var rangeA = new Range(100, 50);
        var rangeB = new Range(10, 10);
        var actualRange = rangeA / rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_divide_from_number()
    {
        var expectedRange = new Range(5, 10);
        var rangeA = new Range(50, 100);
        var number = 10;
        var actualRange = rangeA / number;
        
        Assert.Equal(expectedRange, actualRange);
    }
}