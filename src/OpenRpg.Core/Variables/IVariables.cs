namespace OpenRpg.Core.Variables
{
    public interface IVariables
    {
        int VariableType { get; }
    }
    
    public interface IVariables<T> : IKeyedVariables<int, T>
    { }
}