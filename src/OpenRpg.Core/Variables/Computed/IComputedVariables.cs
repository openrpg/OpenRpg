namespace OpenRpg.Core.Variables.Computed
{
    public interface IComputedVariables : IVariables
    {
    }
    
    public interface IComputedVariables<T> : IKeyedVariables<int, T>, IComputedVariables
    {
        
    }
}