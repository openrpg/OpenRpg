using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.CurveFunctions;

namespace OpenRpg.AdviceEngine.Considerations
{
    public interface IValueBasedConsideration : IConsideration
    {
        IValueAccessor ValueAccessor { get; }
        IClamper Clamper { get; }
        ICurveFunction Evaluator { get; }
    }
}