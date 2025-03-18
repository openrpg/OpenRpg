using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions.Scaling;

namespace OpenRpg.CurveFunctions.Extensions
{
    public static class IScalingFunctionExtensions
    {
        public static ScalingFunction Transpose(this ScalingFunction scalingFunction)
        {
            return new ScalingFunction(scalingFunction.CurveFunction, scalingFunction.InputScale, scalingFunction.OutputScale);
        }
        
        public static float PlotRandom(this ScalingFunction scalingFunction, IRandomizer randomizer)
        {
            var randomNumber = randomizer.Random(scalingFunction.InputScale);
            return scalingFunction.Plot(randomNumber);
        }
    }
}