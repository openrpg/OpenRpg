namespace OpenRpg.Core.Templates
{
    public class TemplateInstanceView<TTemplate, TInstance> : ITemplateInstanceView<TTemplate, TInstance> 
        where TTemplate : ITemplate, new() 
        where TInstance : IHasTemplateLink, new()
    {
        public TInstance Instance { get; set; } = new TInstance();
        public TTemplate Template { get; set; } = new TTemplate();
    }
}