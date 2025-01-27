namespace OpenRpg.Core.Templates
{
    public interface IHasTemplateData<out T> where T : ITemplateData
    {
        T Data { get; }
    }
}