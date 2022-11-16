using Moq;
using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class RandomizerExtensionTests
{
    [Fact]
    public void should_correctly_plot_random_number_using_float_curve_extension()
    {
        var expectedResult = 1f;
        
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(0.0f, 1.0f)).Returns(0.5f);
        var actualResult = mockRandomizer.Object.Random(PresetCurves.BellCurve);
        
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Fact]
    public void should_correctly_plot_random_number_using_curve_extension_with_min_max_floats()
    {
        var expectedResult = 10.0f;
        
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(0.0f, 10.0f)).Returns(5.0f);
        var actualResult = mockRandomizer.Object.Random(PresetCurves.BellCurve, 0.0f, 10.0f);
        
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Fact]
    public void should_correctly_plot_random_number_using_curve_extension_with_negative_min_max_floats()
    {
        var expectedResult = -10.0f;
        
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(-10.0f, 0.0f)).Returns(-5.0f);
        var actualResult = mockRandomizer.Object.Random(PresetCurves.BellCurve, -10.0f, 0.0f);
        
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Theory]
    [InlineData(0, 10, 5, 10)]
    [InlineData(0, 10, 0, 0)]
    [InlineData(0, 10, 10, 0)]
    [InlineData(0, 10, 2, 1)]
    [InlineData(0, 10, 3, 3)]
    [InlineData(0, 10, 4, 7)]
    [InlineData(0, 3, 2, 1)]
    [InlineData(0, 3, 1, 1)]
    public void should_correctly_plot_random_number_using_curve_extension_with_negative_min_max_ints(int min, int max, int randomNumber, int expected)
    {
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(It.IsAny<float>(), It.IsAny<float>())).Returns(randomNumber);
        var actualResult = mockRandomizer.Object.Random(PresetCurves.BellCurve, min, max);
        
        Assert.Equal(expected, actualResult);
    }
}