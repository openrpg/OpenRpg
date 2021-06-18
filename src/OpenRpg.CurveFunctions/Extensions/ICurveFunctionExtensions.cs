namespace OpenRpg.CurveFunctions.Extensions
{
    public static class ICurveFunctionExtensions
    {
        public static float SanitizeValue(this ICurveFunction curve, float value)
        {
            if(float.IsInfinity(value)) { return 0.0f; }
            if(float.IsNaN(value)) { return 0.0f; }
            if(value < 0 ) { return 0.0f; }
            if(value > 1.0f ) { return 1.0f; }
            return value;
        }

        /// <summary>
        /// This will take the value and normalize it based off the maxValue then plot it and denormalize it
        /// </summary>
        /// <param name="curve">Curve type to use</param>
        /// <param name="value">The value to be normalized and plotted</param>
        /// <param name="maxValue">The maximum value used when scaling</param>
        /// <returns>The denormalized value from the curve</returns>
        public static float ScaledPlot(this ICurveFunction curve, float value, float maxValue)
        {
            if (value == 0) { return 0; }
            var normalizedValue = value / maxValue;
            var normalizedOutput = curve.Plot(normalizedValue);
            return normalizedOutput * maxValue;
        }
    }
}