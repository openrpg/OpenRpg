namespace OpenRpg.Core.Variables.General
{
    public interface IHasVariables<out T> where T : IVariables<object>
    {
        T Variables { get; }
    }
}