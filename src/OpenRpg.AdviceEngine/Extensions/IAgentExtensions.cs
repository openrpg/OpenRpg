using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Keys;

namespace OpenRpg.AdviceEngine.Extensions
{
    public static class IAgentExtensions
    {
        public static void AddConsideration(this IAgent agent, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        { agent.ConsiderationHandler.AddConsideration(consideration, explicitUpdateTrigger); }
 
        public static void RemoveConsideration(this IAgent agent, int utilityId)
        { agent.ConsiderationHandler.RemoveConsideration(new UtilityKey(utilityId)); }
        
        public static void RemoveConsideration(this IAgent agent, UtilityKey utilityKey)
        { agent.ConsiderationHandler.RemoveConsideration(utilityKey); }
        
        public static void RemoveConsideration(this IAgent agent, IConsideration consideration)
        { agent.ConsiderationHandler.RemoveConsideration(consideration); }

        public static void AddAdvice(this IAgent agent, IAdvice advice)
        { agent.AdviceHandler.AddAdvice(advice); }

        public static void RemoveAdvice(this IAgent agent, int adviceId)
        {
            var advice = agent.AdviceHandler.GetAdvice(adviceId);
            if (advice != null)
            { agent.AdviceHandler.RemoveAdvice(advice); }
        }
        
        public static void RemoveAdvice(this IAgent agent, IAdvice advice)
        { agent.AdviceHandler.RemoveAdvice(advice); }

        public static IEnumerable<IAdvice> GetAdvice(this IAgent agent)
        { return agent.AdviceHandler.GetAllAdvice().OrderByDescending(x => x.Score); }
    }
}