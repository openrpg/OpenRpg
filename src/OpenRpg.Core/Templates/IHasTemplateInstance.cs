namespace OpenRpg.Core.Templates
{
    public interface IHasTemplateInstance<out T> where T : ITemplateInstance
    {
        T Instance { get; }
    }
}