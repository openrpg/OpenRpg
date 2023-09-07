using System.Collections.Generic;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class CurveExtensionTests
{
    public static IEnumerable<object[]> ScaledTestData()
    {
        yield return new object[] { PresetCurves.Linear, 10, 100, 10 };
        yield return new object[] { PresetCurves.Linear, 50, 100, 50 };
        yield return new object[] { PresetCurves.InverseLinear, 50, 100, 50 };
        yield return new object[] { PresetCurves.InverseLinear, 10, 100, 90 };
        yield return new object[] { PresetCurves.InverseLinear, 10, 100000, 99990 };
        yield return new object[] { PresetCurves.InverseLinear, 0, 10, 10 };
        yield return new object[] { PresetCurves.BellCurve, 5, 10, 10 };
    }
    
    [Theory]
    [MemberData(nameof(ScaledTestData))]
    public void should_scale_plot_correctly(ICurveFunction curve, float inputValue, float maxValue, float expectedValue)
    {
        var actualValue = curve.ScaledPlot(inputValue, maxValue);
        Assert.Equal(expectedValue, actualValue);
    }
    
    [Theory]
    [InlineData(1, 0, 1, 1)]
    [InlineData(0, 0, 1, 0)]
    [InlineData(0.5, 0, 1, 0.5)]
    [InlineData(50, 0, 100, 0.5)]
    [InlineData(50, 0, 1000, 0.05)]
    [InlineData(0, -100, 100, 0.5)]
    [InlineData(90, -100, 100, 0.95)]
    public void should_normalize_within_range_correctly(float input, float min, float max, float expectedValue)
    {
        var normalizedValue = input.NormalizeBetween(min, max);
        Assert.Equal(expectedValue, normalizedValue);
    }
    
    [Theory]
    [InlineData(1, 0, 1, 1)]
    [InlineData(0, 0, 1, 0)]
    [InlineData(0.5, 0, 1, 0.5)]
    [InlineData(0.5, 0, 100, 50)]
    [InlineData(0.05, 0, 1000, 50)]
    [InlineData(0.5, -100, 100, 0)]
    [InlineData(0.95, -100, 100, 90)]
    public void should_denormalize_within_range_correctly(float input, float min, float max, float expectedValue)
    {
        var normalizedValue = input.DenormalizeBetween(min, max);
        Assert.Equal(expectedValue, normalizedValue);
    }
}