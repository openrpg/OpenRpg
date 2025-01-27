namespace OpenRpg.Core.Templates
{
    public interface IHasTemplate<out T> where T : ITemplate
    {
        T Template { get; }
    }
}