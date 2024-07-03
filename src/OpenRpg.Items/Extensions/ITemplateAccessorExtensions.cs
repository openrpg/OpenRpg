using OpenRpg.Core.Templates;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class ITemplateAccessorExtensions
    {
        public static ItemTemplate GetItemTemplate(this ITemplateAccessor templateAccessor, int itemTemplateId)
        { return templateAccessor.Get<ItemTemplate>(itemTemplateId); }
        
        public static ItemView ToView(this ITemplateAccessor templateAccessor, Item item)
        {
            var template = templateAccessor.Get<ItemTemplate>(item.TemplateId);
            return new ItemView() { Instance = item, Template = template };
        }
    }
}