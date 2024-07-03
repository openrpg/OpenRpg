namespace OpenRpg.Core.Templates
{
    public interface ITemplateInstanceView<out TTemplate, out TInstance> : IHasTemplateInstance<TInstance>, IHasTemplate<TTemplate>
        where TTemplate : ITemplate where TInstance : IHasTemplateLink
    {
        
    }
}