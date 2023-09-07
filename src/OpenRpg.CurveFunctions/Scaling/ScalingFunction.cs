using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Scaling
{
    public class ScalingFunction : IScalingFunction
    {
        public ICurveFunction CurveFunction { get; }

        public float InputScaleMin { get; }
        public float InputScaleMax { get; }
        public float OutputScaleMin { get; }
        public float OutputScaleMax { get; }

        public ScalingFunction(ICurveFunction curveFunction, float outputScaleMin, float outputScaleMax, float inputScaleMin = 0, float inputScaleMax = 1)
        {
            CurveFunction = curveFunction;
            InputScaleMin = inputScaleMin;
            InputScaleMax = inputScaleMax;
            OutputScaleMin = outputScaleMin;
            OutputScaleMax = outputScaleMax;
        }
      
        public float Plot(float value)
        {
            var scaledInput = value.NormalizeBetween(InputScaleMin, InputScaleMax);
            scaledInput = scaledInput.SanitizeAndClamp();
            var output = CurveFunction.Plot(scaledInput);
            return output.DenormalizeBetween(OutputScaleMin, OutputScaleMax);
        }
    }
}