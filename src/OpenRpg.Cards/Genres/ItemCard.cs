using System;
using System.Collections.Generic;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Cards.Genres
{
    public class ItemCard : ICard
    {
        public virtual int CardType => CardTypes.ItemCard;
        public ICardVariables Variables { get; set; } = new DefaultCardVariables();
        public IItem Data { get; }

        public ItemCard(IItem item)
        { Data = item; }

        public string NameLocaleId => Data.ItemTemplate.NameLocaleId;
        public string DescriptionLocaleId => Data.ItemTemplate.DescriptionLocaleId;
        public IEnumerable<Effect> Effects => Data.GetItemEffects();
    }
}