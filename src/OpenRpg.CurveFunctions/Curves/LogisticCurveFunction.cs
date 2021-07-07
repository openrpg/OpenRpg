using System;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Curves
{
    public class LogisticCurveFunction : ICurveFunction
    {
        public float Slope { get; }
        public float VerticalSize { get; }
        public float YShift { get; }
        public float XShift { get; }

        public LogisticCurveFunction(float slope, float xShift, float yShift, float verticalSize)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            VerticalSize = verticalSize;
        }

        public float Plot(float value)
        {
            var outputValue = (float)(Slope / (1 + Math.Exp(-10.0 * VerticalSize * (value - 0.5 - XShift))) + YShift);
            return outputValue.SanitizeAndClamp();
        }
    }
}