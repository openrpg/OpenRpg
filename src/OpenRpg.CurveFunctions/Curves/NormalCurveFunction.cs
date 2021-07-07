using System;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Curves
{
    public class NormalCurveFunction : ICurveFunction
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }
        public float Exponent { get; }

        public NormalCurveFunction(float slope, float xShift, float yShift, float exponent)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            Exponent = exponent;
        }

        public float Plot(float value)
        {
            var outputValue = (float)(Slope * Math.Exp(-30.0 * Exponent * (value - XShift - 0.5) * (value - XShift - 0.5)) + YShift);
            return outputValue.SanitizeAndClamp();
        }
    }
}