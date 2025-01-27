using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Extensions;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace OpenRpg.AdviceEngine.Handlers.Advisors
{
    public class AdviceHandler : IAdviceHandler
    {
        private bool _isRunning = false;
        private readonly IList<IAdvice> _advisories = new List<IAdvice>();
        private readonly IDisposable _generalUpdateSub;

        public IUtilityVariables UtilityVariables { get; }
        public object OwnerContext { get; }
        
        public AdviceHandler(IRefreshScheduler scheduler, IUtilityVariables utilityVariables, object ownerContext)
        {
            OwnerContext = ownerContext;
            UtilityVariables = utilityVariables;
            _generalUpdateSub = scheduler.DefaultRefreshPeriod.Subscribe(x => GeneralRefreshAdvice());
        }
        
        public void StartHandler() => _isRunning = true;
        public void StopHandler() => _isRunning = false;
        
        public void AddAdvice(IAdvice advice)
        { _advisories.Add(advice); }

        public void RemoveAdvice(IAdvice advice)
        { _advisories.Remove(advice); }

        public void ClearAdvice()
        { _advisories.Clear(); }

        public IAdvice GetAdvice(int adviceId)
        { return _advisories.FirstOrDefault(x => x.AdviceId == adviceId); }

        public IEnumerable<IAdvice> GetAllAdvice()
        { return _advisories; }

        public void RefreshAdvice(IAdvice advice)
        {
            if(!_isRunning) { return; }

            var utilityValues = new List<float>();
            foreach (var utilityKey in advice.UtilityKeys)
            {
                var value = utilityKey.RelatedId == 0 ? 
                    UtilityVariables.GetRelatedUtilities(utilityKey.UtilityId).Select(x => x.Value).DefaultIfEmpty(0).Max() : 
                    UtilityVariables[utilityKey];

                utilityValues.Add(value);
            }

            advice.Score = utilityValues.CalculateScore();

            foreach (var modifier in advice.ScoreModifiers)
            {
                if (modifier.ShouldApply(OwnerContext, UtilityVariables))
                { advice.Score = modifier.ModifyValue(advice.Score, OwnerContext, UtilityVariables); }
            }
        }

        public void GeneralRefreshAdvice()
        {
            foreach (var advice in _advisories)
            { RefreshAdvice(advice); }
        }

        public void Dispose()
        { _generalUpdateSub.Dispose(); }
    }
}