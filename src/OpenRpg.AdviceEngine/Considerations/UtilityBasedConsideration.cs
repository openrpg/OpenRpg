using System;
using System.Collections.Generic;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Modifiers;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.AdviceEngine.Considerations
{
    public class UtilityBasedConsideration : IUtilityBasedConsideration
    {
        public UtilityKey UtilityId { get; }
        public IEnumerable<IValueModifier> Modifiers { get; set; }
        public UtilityKey DependentUtilityId { get; }
        public ICurveFunction Evaluator { get; }

        public UtilityBasedConsideration(UtilityKey utilityId, UtilityKey dependentUtilityId, ICurveFunction evaluator = null, IEnumerable<IValueModifier> modifiers = null)
        {
            DependentUtilityId = dependentUtilityId;
            UtilityId = utilityId;
            Evaluator = evaluator;
            Modifiers = modifiers ?? Array.Empty<IValueModifier>();
        }

        public float CalculateUtility(object ownerContext, IUtilityVariables utilityVariables)
        {
            var existingUtility = utilityVariables[DependentUtilityId];
            if (Evaluator != null)
            { existingUtility = Evaluator.Plot(existingUtility); }

            foreach (var modifier in Modifiers)
            {
                if(modifier.ShouldApply(ownerContext, utilityVariables))
                { existingUtility = modifier.ModifyValue(existingUtility, ownerContext, utilityVariables); }
            }

            return existingUtility.SanitizeAndClamp();
        }
    }
}