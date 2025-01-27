using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Modifiers
{
    public interface IValueModifier
    {
        bool ShouldApply(object ownerContext, IUtilityVariables utilityVariables);
        float ModifyValue(float currentValue, object ownerContext, IUtilityVariables utilityVariables);
    }
}