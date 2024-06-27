namespace OpenRpg.Core.Templates
{
    public interface ITemplateInstanceFacade<out TTemplate, out TInstance> : IHasTemplateInstance<TInstance>, IHasTemplate<TTemplate>
        where TTemplate : ITemplate where TInstance : ITemplateInstance
    {
        
    }
}