using System.Collections.Generic;
using OpenRpg.Cards.Types;
using OpenRpg.Cards.Variables;
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

        public string NameLocaleId => Data.Template.NameLocaleId;
        public string DescriptionLocaleId => Data.Template.DescriptionLocaleId;
        public IEnumerable<Effect> Effects => Data.GetItemEffects();
    }
}