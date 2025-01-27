using System.Collections.Generic;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Modifiers;

namespace OpenRpg.AdviceEngine.Advisors
{
    public interface IAdvice
    {
        int AdviceId { get; }
        float Score { get; set; }
        IEnumerable<UtilityKey> UtilityKeys { get; }
        IEnumerable<IValueModifier> ScoreModifiers { get; }
        IContextAccessor ContextAccessor { get; }
    }
}