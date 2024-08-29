using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Modifiers;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.AdviceEngine.Considerations
{
    public class ValueBasedConsideration : IValueBasedConsideration
    {
        public UtilityKey UtilityId { get; }
        public IEnumerable<IValueModifier> Modifiers { get; }
        public IValueAccessor ValueAccessor { get; }
        public IClamper Clamper { get; }
        public ICurveFunction Evaluator { get; }

        public ValueBasedConsideration(UtilityKey utilityId, IValueAccessor valueAccessor, IClamper clamper, ICurveFunction evaluator, IEnumerable<IValueModifier> modifiers = null)
        {
            ValueAccessor = valueAccessor;
            Clamper = clamper;
            Evaluator = evaluator;
            UtilityId = utilityId;
            Modifiers = modifiers ?? Array.Empty<IValueModifier>();
        }

        public float CalculateUtility(object ownerContext, IUtilityVariables utilityVariables)
        {
            var value = ValueAccessor.GetValue(ownerContext, utilityVariables);
            var clampedValue = Clamper.Clamp(value);
            var utilityValue = Evaluator.Plot(clampedValue);

            foreach (var modifier in Modifiers)
            {
                if (modifier.ShouldApply(ownerContext, utilityVariables))
                { utilityValue = modifier.ModifyValue(utilityValue, ownerContext, utilityVariables); }
            }

            return utilityValue.SanitizeAndClamp();
        }
    }
}