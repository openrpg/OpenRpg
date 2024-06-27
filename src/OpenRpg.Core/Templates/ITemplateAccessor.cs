namespace OpenRpg.Core.Templates
{
    public interface ITemplateAccessor
    {
        public T Get<T>(int templateId) where T : ITemplate;
    }
}