namespace OpenRpg.CurveFunctions.Curves
{
    public class StepCurveFunction : ICurveFunction
    {
        public float StepValue { get; }
        public float MinValue { get; }
        public float MaxValue { get; }

        public StepCurveFunction(float stepValue, float minValue = 0.0f, float maxValue = 1.0f)
        {
            StepValue = stepValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public float Plot(float value)
        { return value < StepValue ? MinValue : MaxValue; }
    }
}