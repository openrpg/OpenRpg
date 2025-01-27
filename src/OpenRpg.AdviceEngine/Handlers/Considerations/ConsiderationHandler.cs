using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Extensions;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Variables;

namespace OpenRpg.AdviceEngine.Handlers.Considerations
{
    public class ConsiderationHandler : IConsiderationHandler
    {
        private readonly IDictionary<UtilityKey, IConsideration> _considerations = new Dictionary<UtilityKey, IConsideration>();
        private readonly IDictionary<UtilityKey, IDisposable> _explicitUpdateSchedules = new Dictionary<UtilityKey, IDisposable>();
        private readonly IList<UtilityKey> _generalUpdateConsiderations = new List<UtilityKey>();
        private readonly IDisposable _generalUpdateSub;
        private bool _isRunning = false;
        
        public IUtilityVariables UtilityVariables { get; }
        public object OwnerContext { get; }

        public ConsiderationHandler(IRefreshScheduler scheduler, IUtilityVariables utilityVariables,
            object ownerContext)
        {
            OwnerContext = ownerContext;
            UtilityVariables = utilityVariables;
            _generalUpdateSub = scheduler.DefaultRefreshPeriod.Subscribe(x => GeneralRefreshConsiderations());
        }
        
        public void StartHandler() => _isRunning = true;
        public void StopHandler() => _isRunning = false;
        
        public void AddConsideration(IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            _considerations.Add(consideration.UtilityId, consideration);
            UtilityVariables[consideration.UtilityId] = 0;
            HandleSchedulingForConsideration(consideration, explicitUpdateTrigger);
            RefreshConsideration(consideration);
        }

        public void RemoveConsideration(UtilityKey utilityKey)
        {
            if (_explicitUpdateSchedules.ContainsKey(utilityKey))
            {
                _explicitUpdateSchedules[utilityKey].Dispose();
                _explicitUpdateSchedules.Remove(utilityKey);
            }

            if (_generalUpdateConsiderations.Contains(utilityKey))
            { _generalUpdateConsiderations.Remove(utilityKey); }

            _considerations.Remove(utilityKey);
            
            if(UtilityVariables.ContainsKey(utilityKey))
            { UtilityVariables.Remove(utilityKey); }
        }

        public void ClearConsiderations()
        {
            var utilityKeys = _considerations.Keys.ToArray();
            foreach (var key in utilityKeys)
            { RemoveConsideration(key); }
        }

        private void HandleSchedulingForConsideration(IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            if (explicitUpdateTrigger != null)
            {
                var sub = explicitUpdateTrigger.Subscribe(x => RefreshConsideration(consideration));
                _explicitUpdateSchedules[consideration.UtilityId] = sub;
                return;
            }
            
            if (_considerations[consideration.UtilityId] is IValueBasedConsideration valueConsideration)
            { HandleDefaultSchedulingForValueConsideration(valueConsideration); }
            else if(_considerations[consideration.UtilityId] is IUtilityBasedConsideration utilityConsideration)
            { HandleDefaultSchedulingForUtilityConsideration(utilityConsideration); }
            
        }

        private void HandleDefaultSchedulingForValueConsideration(IValueBasedConsideration consideration)
        {
            _generalUpdateConsiderations.Add(consideration.UtilityId);
        }
        
        private void HandleDefaultSchedulingForUtilityConsideration(IUtilityBasedConsideration consideration)
        {
            var compositeDisposable = new CompositeDisposable();
            Observable.FromEventPattern<VariableChangedEventHandler<UtilityKey, float>, VariableEventArgs<UtilityKey, float>>(
                    x => UtilityVariables.OnChanged += x,
                    x => UtilityVariables.OnChanged -= x)
                .Where(x => x.EventArgs.VariableType.Equals(consideration.DependentUtilityId))
                .Subscribe(x => RefreshConsideration(consideration))
                .AddTo(compositeDisposable);
            
            Observable.FromEventPattern<VariableChangedEventHandler<UtilityKey, float>, VariableEventArgs<UtilityKey, float>>(
                x => UtilityVariables.OnRemoved += x,
                x => UtilityVariables.OnRemoved -= x)
                .FirstAsync()
                .Subscribe(x => RemoveConsideration(consideration.UtilityId))
                .AddTo(compositeDisposable);

            _explicitUpdateSchedules[consideration.UtilityId] = compositeDisposable;
        }

        private void RefreshConsideration(IConsideration consideration)
        {
            if(!_isRunning) { return; }
            
            var newUtility = 0.0f;
            if (consideration is ExternalUtilityBasedConsideration externalUtilityBasedConsideration)
            {
                var externalVariables = externalUtilityBasedConsideration.ExternalVariableAccessor();
                newUtility = consideration.CalculateUtility(OwnerContext, externalVariables);
            }
            else
            { newUtility = consideration.CalculateUtility(OwnerContext, UtilityVariables); }

            UtilityVariables[consideration.UtilityId] = newUtility;
        }

        private void GeneralRefreshConsiderations()
        {
            foreach (var utilityId in _generalUpdateConsiderations)
            { RefreshConsideration(_considerations[utilityId]); }
        }

        public void Dispose()
        {
            foreach (var disposable in _explicitUpdateSchedules.Values)
            { disposable.Dispose(); }
            
            _generalUpdateSub.Dispose();
        }
    }
}