using System.Collections.Generic;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Modifiers;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Considerations
{
    public interface IConsideration
    {
        UtilityKey UtilityId { get; }
        IEnumerable<IValueModifier> Modifiers { get; }
        float CalculateUtility(object ownerContext, IUtilityVariables utilityVariables);
    }
}