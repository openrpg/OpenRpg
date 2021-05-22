using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Classes;

namespace OpenRpg.Cards.Genres
{
    public class ClassCard : GenericDataCardWithEffects<IClassTemplate>
    {
        public override int CardType => CardTypes.ClassCard;

        public ClassCard(IClassTemplate data) : base(data) { }
    }
}