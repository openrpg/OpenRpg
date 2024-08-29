using System;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Modifiers
{
    public class ManualValueModifier : IValueModifier
    {
        private readonly Func<object, IUtilityVariables, bool> _shouldApplyFunction;
        private readonly Func<float, object, IUtilityVariables, float> _modifyScoreFunction;

        public ManualValueModifier(Func<object, IUtilityVariables, bool> shouldApplyFunction, Func<float, object, IUtilityVariables, float> modifyScoreFunction)
        {
            _modifyScoreFunction = modifyScoreFunction;
            _shouldApplyFunction = shouldApplyFunction;
        }

        public bool ShouldApply(object ownerContext, IUtilityVariables utilityVariables) => 
            _shouldApplyFunction(ownerContext, utilityVariables);

        public float ModifyValue(float currentScore, object ownerContext, IUtilityVariables utilityVariables) =>
            _modifyScoreFunction(currentScore, ownerContext, utilityVariables);
    }
}