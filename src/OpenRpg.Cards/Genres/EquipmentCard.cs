using OpenRpg.Cards.Types;
using OpenRpg.Items;

namespace OpenRpg.Cards.Genres
{
    public class EquipmentCard : ItemCard
    {
        public override int CardType => CardTypes.EquipmentCard;

        public EquipmentCard(ItemView item) : base(item)
        {
        }
    }
}