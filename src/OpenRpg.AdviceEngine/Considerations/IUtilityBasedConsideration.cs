using OpenRpg.AdviceEngine.Keys;
using OpenRpg.CurveFunctions;

namespace OpenRpg.AdviceEngine.Considerations
{
    public interface IUtilityBasedConsideration : IConsideration
    {
        UtilityKey DependentUtilityId { get; }
        ICurveFunction Evaluator { get; }
    }
}