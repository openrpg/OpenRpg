using System.Linq;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;
using OpenRpg.CurveFunctions.Scaling;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class ScalingFunctionTests
{
    [Theory]
    [InlineData(1, 0, 1, 0, 1, 1)]
    [InlineData(100, 0, 1, 0, 1, 1)]
    [InlineData(0, 0, 1, 0, 1, 0)]
    [InlineData(-1, 0, 1, 0, 1, 0)]
    [InlineData(0.5, 0, 1, 0, 1, 0.5)]
    [InlineData(20, 0, 1, 0, 100, 0.2)]
    [InlineData(25, 1000, 2000, 0, 100, 1250)]
    [InlineData(25, 1000, 2000, 0, 50, 1500)]
    public void should_correctly_calculate_logistic_curve_value(float input, float outputMin, float outputMax, float inputMin, float inputMax, float expectedOutput)
    {
        var scalingFunction = new ScalingFunction(PresetCurves.Linear, outputMin, outputMax, inputMin, inputMax);
        var actualValue = scalingFunction.Plot(input);
        Assert.Equal(expectedOutput.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(1, 0, 1, 0, 1, 0)]
    [InlineData(0, 0, 1, 0, 1, 1)]
    [InlineData(0.5, 0, 1, 0, 1, 0.5)]
    [InlineData(20, 0, 1, 0, 100, 0.8)]
    [InlineData(25, 1000, 2000, 0, 100, 1750)]
    [InlineData(25, 1000, 2000, 0, 50, 1500)]
    public void should_correctly_calculate_logistic_curve_value_with_inverse_linear_function(float input, float outputMin, float outputMax, float inputMin, float inputMax, float expectedOutput)
    {
        var scalingFunction = new ScalingFunction(PresetCurves.InverseLinear, outputMin, outputMax, inputMin, inputMax);
        var actualValue = scalingFunction.Plot(input);
        Assert.Equal(expectedOutput.ToString("F"), actualValue.ToString("F"));
    }

    [Theory]
    [InlineData(1, 0, 1, 0, 1, 0)]
    [InlineData(0, 0, 1, 0, 1, 1)]
    [InlineData(0.5, 0, 1, 0, 1, 0.5)]
    [InlineData(20, 0, 1, 0, 100, 0.8)]
    [InlineData(25, 1000, 2000, 0, 100, 1750)]
    [InlineData(25, 1000, 2000, 0, 50, 1500)]
    public void should_transpose_scaling_function_correctly(float input, float outputMin, float outputMax, float inputMin, float inputMax, float expectedOutput)
    {
        var scalingFunction = new ScalingFunction(PresetCurves.InverseLinear, outputMin, outputMax, inputMin, inputMax);
        var transposedScalingFunction = scalingFunction.Transpose();
        var actualValue = scalingFunction.Plot(input);
        var actualTransposedValue = transposedScalingFunction.Plot(actualValue);
        Assert.Equal(expectedOutput.ToString("F"), actualValue.ToString("F"));
        Assert.Equal(input.ToString("F"), actualTransposedValue.ToString("F"));
    }
    
    [Fact]
    public void sanity_test()
    {
        var scalingFunction = new ScalingFunction(PresetCurves.QuadraticUpperLeft, 25, 100, 0, 50);
        var points = Enumerable.Range(0, 50).Select(x => (float)x).PlotAgainst(scalingFunction).ToArray();
        Assert.Equal(50, points.Length);
    }
}