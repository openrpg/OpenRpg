using System;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Curves
{
    public class SineCurveFunction : ICurveFunction
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }

        public SineCurveFunction(float slope, float xShift, float yShift)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
        }

        public float Plot(float value)
        {
            var outputValue = (float)(0.5 * Slope * Math.Sin(2.0 * Math.PI * (value - XShift)) + 0.5 + YShift);
            return outputValue.SanitizeAndClamp();
        }
    }
}