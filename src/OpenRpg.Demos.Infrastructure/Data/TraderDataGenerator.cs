using System.Collections.Generic;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Demos.Infrastructure.Trading;
using OpenRpg.Items.TradeSkills.Trading;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public static class TraderDataGenerator
    {
        public const int GoldStartingAmount = 500;

        public static Trader CreateWeaponMerchant()
        {
            return new Trader("Weapon Merchant Gruk", new[]
            {
                new ItemTradeEntry { ItemTemplateId = ItemTemplateLookups.Sword, BuyRate = 1.5f, SellRate = 0.4f },
                new ItemTradeEntry { ItemTemplateId = ItemTemplateLookups.SuperSword, BuyRate = 1.2f, SellRate = 0.5f },
                new ItemTradeEntry { ItemTemplateId = ItemTemplateLookups.CopperSword, BuyRate = 1.4f, SellRate = 0.45f },
                new ItemTradeEntry { ItemTemplateId = ItemTemplateLookups.Helm, BuyRate = 1.3f, SellRate = 0.4f },
                new ItemTradeEntry { ItemTemplateId = ItemTemplateLookups.HealingPotion, BuyRate = 1.0f, SellRate = 0.5f }
            });
        }
    }
}
