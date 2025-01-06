using OpenRpg.Core.Templates;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class ITemplateAccessorExtensions
    {
        public static ItemTemplate GetItemTemplate(this ITemplateAccessor templateAccessor, int itemTemplateId)
        { return templateAccessor.Get<ItemTemplate>(itemTemplateId); }
        
        public static Item ToInstance(this ITemplateAccessor templateAccessor, ItemData itemData)
        {
            var template = templateAccessor.Get<ItemTemplate>(itemData.TemplateId);
            return new Item() { Data = itemData, Template = template };
        }
    }
}