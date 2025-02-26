using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.CurveFunctions.Scaling
{
    public class ScalingFunction : IScalingFunction
    {
        public ICurveFunction CurveFunction { get; }

        public RangeF InputScale { get; }
        public RangeF OutputScale { get; }

        public ScalingFunction(ICurveFunction curveFunction, float outputScaleMin, float outputScaleMax, float inputScaleMin = 0, float inputScaleMax = 1)
            : this(curveFunction, new RangeF(outputScaleMin, outputScaleMax), new RangeF(inputScaleMin, inputScaleMax))
        {}
        
        public ScalingFunction(ICurveFunction curveFunction, RangeF outputScale, RangeF inputScale)
        {
            CurveFunction = curveFunction;
            OutputScale = outputScale;
            InputScale = inputScale;
        }
      
        public float Plot(float value)
        {
            var scaledInput = value.NormalizeBetween(InputScale);
            scaledInput = scaledInput.SanitizeAndClamp();
            var output = CurveFunction.Plot(scaledInput);
            return output.DenormalizeBetween(OutputScale);
        }
    }
}