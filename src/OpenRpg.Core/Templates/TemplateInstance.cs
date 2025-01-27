namespace OpenRpg.Core.Templates
{
    public class TemplateInstance<TTemplate, TTemplateData> : ITemplateInstance<TTemplate, TTemplateData> 
        where TTemplate : ITemplate, new() 
        where TTemplateData : ITemplateData, new()
    {
        public TTemplateData Data { get; set; } = new TTemplateData();
        public TTemplate Template { get; set; } = new TTemplate();
    }
}