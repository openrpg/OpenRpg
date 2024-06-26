namespace OpenRpg.Core.Templates
{
    public interface IHasTemplate<out T> where T : IDataTemplate
    {
        T Template { get; }
    }
}