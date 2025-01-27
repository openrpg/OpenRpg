using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Accessors
{
    public interface IValueAccessor : IHasDataId
    {
        float GetValue(object ownerContext, IUtilityVariables utilityVariables);
    }
}