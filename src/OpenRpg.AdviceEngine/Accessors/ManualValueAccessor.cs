using System;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Accessors
{
    public class ManualValueAccessor : IValueAccessor
    {
        public int Id => 0;
        
        public Func<object, IUtilityVariables, float> GetValueFunction { get; }

        public ManualValueAccessor(Func<object, IUtilityVariables, float> getValueFunction)
        { GetValueFunction = getValueFunction; }

        public float GetValue(object context, IUtilityVariables variables) => GetValueFunction(context, variables);
    }
}