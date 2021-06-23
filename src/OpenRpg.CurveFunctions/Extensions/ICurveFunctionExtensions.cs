using OpenRpg.CurveFunctions.Curves;

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

        public static PolynomialCurveFunction Alter(this PolynomialCurveFunction curve, float? slope = null, float? xShift = null, float? yShift = null, float? exponent = null)
        {
            return new PolynomialCurveFunction(
                slope.HasValue ? slope.Value : curve.Slope,
                xShift.HasValue ? xShift.Value : curve.XShift,
                yShift.HasValue ? yShift.Value : curve.YShift,
                exponent.HasValue ? exponent.Value : curve.Exponent
            );
        }
        
        public static LogisticCurveFunction Alter(this LogisticCurveFunction curve, float? slope = null, float? xShift = null, float? yShift = null, float? verticalSize = null)
        {
            return new LogisticCurveFunction(
                slope.HasValue ? slope.Value : curve.Slope,
                xShift.HasValue ? xShift.Value : curve.XShift,
                yShift.HasValue ? yShift.Value : curve.YShift,
                verticalSize.HasValue ? verticalSize.Value : curve.VerticalSize
            );
        }
        
        public static NormalCurveFunction Alter(this NormalCurveFunction curve, float? slope = null, float? xShift = null, float? yShift = null, float? exponent = null)
        {
            return new NormalCurveFunction(
                slope.HasValue ? slope.Value : curve.Slope,
                xShift.HasValue ? xShift.Value : curve.XShift,
                yShift.HasValue ? yShift.Value : curve.YShift,
                exponent.HasValue ? exponent.Value : curve.Exponent
            );
        }
        
        public static LogitCurveFunction Alter(this LogitCurveFunction curve, float? slope = null, float? xShift = null, float? yShift = null)
        {
            return new LogitCurveFunction(
                slope.HasValue ? slope.Value : curve.Slope,
                xShift.HasValue ? xShift.Value : curve.XShift,
                yShift.HasValue ? yShift.Value : curve.YShift
            );
        }
        
        public static SineCurveFunction Alter(this SineCurveFunction curve, float? slope = null, float? xShift = null, float? yShift = null)
        {
            return new SineCurveFunction(
                slope.HasValue ? slope.Value : curve.Slope,
                xShift.HasValue ? xShift.Value : curve.XShift,
                yShift.HasValue ? yShift.Value : curve.YShift
            );
        }
        
        public static StepCurveFunction Alter(this StepCurveFunction curve, float? stepValue = null, float? minValue = null, float? maxValue = null)
        {
            return new StepCurveFunction(
                stepValue.HasValue ? stepValue.Value : curve.StepValue,
                minValue.HasValue ? minValue.Value : curve.MinValue,
                maxValue.HasValue ? maxValue.Value : curve.MaxValue
            );
        }
    }
}