using System.Collections.Generic;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class CurveExtensionTests
{
    public static IEnumerable<object[]> TestData()
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
    [MemberData(nameof(TestData))]
    public void should_scale_plot_correctly(ICurveFunction curve, float inputValue, float maxValue, float expectedValue)
    {
        var actualValue = curve.ScaledPlot(inputValue, maxValue);
        Assert.Equal(expectedValue, actualValue);
    }
}