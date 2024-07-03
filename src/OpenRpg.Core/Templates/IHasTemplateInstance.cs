namespace OpenRpg.Core.Templates
{
    public interface IHasTemplateInstance<out T> where T : IHasTemplateLink
    {
        T Instance { get; }
    }
}