using OpenRpg.Core.Utils;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Ranges;

public class RangeFTests
{
    [Fact]
    public void should_correctly_convert_from_float_range_to_int_range()
    {
        var expectedRange = new Range(22, 33);
        var range = new RangeF(22.4f, 33.6f);
        Range actualRange = range;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_add_from_two_ranges()
    {
        var expectedRange = new RangeF(5.0f, 10.0f);
        var rangeA = new RangeF(2.5f, 5.0f);
        var rangeB = new RangeF(2.5f, 5.0f);
        var actualRange = rangeA + rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_add_from_number()
    {
        var expectedRange = new RangeF(5.0f, 7.5f);
        var rangeA = new RangeF(2.5f, 5.0f);
        var number = 2.5f;
        var actualRange = rangeA + number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_subtract_from_two_ranges()
    {
        var expectedRange = new RangeF(0, 2.5f);
        var rangeA = new RangeF(2.5f, 5.0f);
        var rangeB = new RangeF(2.5f, 2.5f);
        var actualRange = rangeA - rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_subtract_from_number()
    {
        var expectedRange = new RangeF(0, 2.5f);
        var rangeA = new RangeF(2.5f, 5.0f);
        var number = 2.5f;
        var actualRange = rangeA - number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_multiply_from_two_ranges()
    {
        var expectedRange = new RangeF(5.0f, 50.0f);
        var rangeA = new RangeF(0.5f, 5.0f);
        var rangeB = new RangeF(10.0f, 10.0f);
        var actualRange = rangeA * rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_multiply_from_number()
    {
        var expectedRange = new RangeF(25.0f, 50.0f);
        var rangeA = new RangeF(2.5f, 5.0f);
        var number = 10.0f;
        var actualRange = rangeA * number;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
        
    [Fact]
    public void should_correctly_divide_from_two_ranges()
    {
        var expectedRange = new RangeF(1.0f, 0.5f);
        var rangeA = new RangeF(10.0f, 5.0f);
        var rangeB = new RangeF(10.0f, 10.0f);
        var actualRange = rangeA / rangeB;
        
        Assert.Equal(expectedRange, actualRange);
    }
    
    [Fact]
    public void should_correctly_divide_from_number()
    {
        var expectedRange = new RangeF(0.5f, 1.0f);
        var rangeA = new RangeF(5.0f, 10.0f);
        var number = 10.0f;
        var actualRange = rangeA / number;
        
        Assert.Equal(expectedRange, actualRange);
    }
}