using System;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Curves
{
    public class LogitCurveFunction : ICurveFunction
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }

        public LogitCurveFunction(float slope, float xShift, float yShift)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
        }

        public float Plot(float value)
        {
            var outputValue = (float)(Slope * Math.Log((value - XShift) / (1.0 - (value - XShift))) / 5.0 + 0.5 + YShift);
            return outputValue.SanitizeAndClamp();
        }
    }
}