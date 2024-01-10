using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Curves;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class CurveFunctionTests
{
    [Theory]
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 5.0f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0f, 0.0f, 1.0f, -10.0f, 0.0f)] // Doesnt go below 0
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 0.4f, 0.27f)]
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 1.0f, 0.99f)]
    [InlineData(1.0f, 0f, 1.0f, 1.0f, 1.0f, 1.0f)]
    public void should_correctly_calculate_logistic_curve_value(float slope, float xShift, float yShift, float verticalSize, float inputValue, float expectedValue)
    {
        var curve = new LogisticCurveFunction(slope, xShift, yShift, verticalSize);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(2.0f, 0f, 2.0f, 0.5f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0f, 0.0f, -10.0f, 0.0f)] // Doesnt go below 0
    [InlineData(1.0f, 0f, 0.0f, 0.4f, 0.42f)]
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 0.0f)]
    [InlineData(1.0f, 0f, 1.0f, 1.0f, 0.0f)]
    public void should_correctly_calculate_logit_curve_value(float slope, float xShift, float yShift, float inputValue, float expectedValue)
    {
        var curve = new LogitCurveFunction(slope, xShift, yShift);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(2.0f, 0.2f, 2.0f, 2.0f, 0.5f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0f, 0.0f, 1.0f, -10.0f, 0.0f)] // Doesnt go below 0
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 0.4f, 0.74f)]
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 1.0f, 0)]
    [InlineData(1.0f, 0.2f, 1.0f, 1.0f, 1.0f, 1.0f)]
    public void should_correctly_calculate_normal_curve_value(float slope, float xShift, float yShift, float verticalSize, float inputValue, float expectedValue)
    {
        var curve = new NormalCurveFunction(slope, xShift, yShift, verticalSize);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 5.0f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0f, 0.0f, 1.0f, -10.0f, 0.0f)] // Doesnt go below 0
    [InlineData(1.0f, 0f, 0.0f, 1.0f, 0.4f, 0.4f)]
    [InlineData(-1.0f, 0f, 1.0f, 4.0f, 0.5f, 0.94f)]
    [InlineData(1.0f, 0f, 1.0f, 1.0f, 1.0f, 1.0f)]
    public void should_correctly_calculate_polynomial_curve_value(float slope, float xShift, float yShift, float verticalSize, float inputValue, float expectedValue)
    {
        var curve = new PolynomialCurveFunction(slope, xShift, yShift, verticalSize);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(1.0f, 0, 2.0f, 0.5f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0, -0.1f, 0.8f, 0.0f)] // Doesnt go below 0
    [InlineData(1.0f, 0, 0, 0.4f, 0.79f)]
    public void should_correctly_calculate_sine_curve_value(float slope, float xShift, float yShift, float inputValue, float expectedValue)
    {
        var curve = new SineCurveFunction(slope, xShift, yShift);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(1.0f, 0, 2.0f, 1.0f, 1.0f)] // Doesnt go over 1
    [InlineData(1.0f, 0, -0.1f, 0.8f, 0.0f)] // Doesnt go below 0
    [InlineData(0.5f, 1.0f, 0.0f, 0.45f, 1.0f)]
    public void should_correctly_calculate_step_curve_value(float slope, float xShift, float yShift, float inputValue, float expectedValue)
    {
        var curve = new StepCurveFunction(slope, xShift, yShift);
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
    
    [Theory]
    [InlineData(0.5f, 0.5f)] // Passes through
    public void should_correctly_pass_through_curve_value(float inputValue, float expectedValue)
    {
        var curve = new PassThroughCurveFunction();
        var actualValue = curve.Plot(inputValue);
        Assert.Equal(expectedValue.ToString("F"), actualValue.ToString("F"));
    }
}