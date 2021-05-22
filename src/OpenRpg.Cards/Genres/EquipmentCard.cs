using System;
using System.Collections.Generic;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Cards.Genres
{
    public class EquipmentCard : ICard
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public string AssetCode { get; set; }

        public int CardType => CardTypes.EquipmentCard;
        public ICardVariables Variables { get; protected set; } = new DefaultCardVariables();
        public IItem Data { get; }

        public EquipmentCard(IItem item)
        { Data = item; }

        public string NameLocaleId => Data.ItemTemplate.NameLocaleId;
        public string DescriptionLocaleId => Data.ItemTemplate.DescriptionLocaleId;
        public IEnumerable<Effect> Effects => Data.GetItemEffects();
    }
}