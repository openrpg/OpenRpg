namespace OpenRpg.Core.Common
{
    public interface IHasTemplate<out T> where T : IDataTemplate
    {
        T Template { get; }
    }
}