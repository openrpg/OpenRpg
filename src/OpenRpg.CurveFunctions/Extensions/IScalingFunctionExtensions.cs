using OpenRpg.CurveFunctions.Scaling;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class IScalingFunctionExtensions
    {
        public static ScalingFunction Transpose(this ScalingFunction scalingFunction)
        {
            return new ScalingFunction(scalingFunction.CurveFunction, scalingFunction.InputScaleMin,
                scalingFunction.InputScaleMax, scalingFunction.OutputScaleMin, scalingFunction.OutputScaleMax);
        }
    }
}