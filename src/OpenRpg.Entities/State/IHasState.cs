namespace OpenRpg.Entities.State
{
    public interface IHasState<out T> where T : IStateVariables
    {
        T State { get; }
    }
}