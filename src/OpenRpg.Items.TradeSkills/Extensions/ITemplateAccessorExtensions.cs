using OpenRpg.Core.Templates;
using OpenRpg.Items.TradeSkills.Templates;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class ITemplateAccessorExtensions
    {
        public static ItemGatheringTemplate GetItemGatheringTemplate(this ITemplateAccessor templateAccessor, int itemGatheringTemplateId)
        { return templateAccessor.Get<ItemGatheringTemplate>(itemGatheringTemplateId); }
        
        public static ItemCraftingTemplate GetItemCraftingTemplate(this ITemplateAccessor templateAccessor, int itemGatheringTemplateId)
        { return templateAccessor.Get<ItemCraftingTemplate>(itemGatheringTemplateId); }
    }
}