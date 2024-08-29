using System;
using System.Collections.Generic;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Modifiers;

namespace OpenRpg.AdviceEngine.Advisors
{
    public class DefaultAdvice : IAdvice
    {
        public int AdviceId { get; }
        public float Score { get; set; }
        public IEnumerable<UtilityKey> UtilityKeys { get; }
        public IEnumerable<IValueModifier> ScoreModifiers { get; }
        public IContextAccessor ContextAccessor { get; }

        public DefaultAdvice(int adviceId, IEnumerable<UtilityKey> utilityKeys, IContextAccessor relatedContextAccessor, IEnumerable<IValueModifier> scoreModifiers = null)
        {
            ContextAccessor = relatedContextAccessor;
            AdviceId = adviceId;
            UtilityKeys = utilityKeys;
            ScoreModifiers = scoreModifiers ?? Array.Empty<IValueModifier>();
        }
    }
}