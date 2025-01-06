namespace OpenRpg.Core.Templates
{
    public interface ITemplateInstance<out TTemplate, out TTemplateData> : IHasTemplateData<TTemplateData>, IHasTemplate<TTemplate>
        where TTemplate : ITemplate where TTemplateData : ITemplateData
    {
        
    }
}