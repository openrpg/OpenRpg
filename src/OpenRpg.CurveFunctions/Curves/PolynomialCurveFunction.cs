using System;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Curves
{
    public class PolynomialCurveFunction : ICurveFunction
    {
        public float Slope { get; }
        public float Exponent { get; }
        public float YShift { get; }
        public float XShift { get; }

        public PolynomialCurveFunction(float slope, float xShift, float yShift, float exponent)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            Exponent = exponent;
        }

        public float Plot(float value)
        {
            var outputValue = Slope * (float)Math.Pow((value - XShift), Exponent) + YShift;
            return this.SanitizeValue(outputValue);
        }
    }
}