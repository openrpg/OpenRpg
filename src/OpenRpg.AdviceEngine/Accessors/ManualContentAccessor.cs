using System;
using OpenRpg.AdviceEngine.Variables;

namespace OpenRpg.AdviceEngine.Accessors
{
    public class ManualContextAccessor : IContextAccessor
    {
        public int Id => 0;

        public object Context { get; }
        public IUtilityVariables Variables { get; }
        public Func<object, IUtilityVariables, object> GetContextFunction { get; }

        public ManualContextAccessor(object context, IUtilityVariables variables,
            Func<object, IUtilityVariables, object> getContextFunction)
        {
            Context = context;
            Variables = variables;
            GetContextFunction = getContextFunction;
        }
        
        public object GetContext() => GetContextFunction(Context, Variables);
    }
}