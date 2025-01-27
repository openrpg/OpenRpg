using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Classes.Templates;

namespace OpenRpg.Cards.Genres
{
    public class ClassCard : GenericDataCardWithEffects<ClassTemplate>
    {
        public override int CardType => CardTypes.ClassCard;

        public ClassCard(ClassTemplate data) : base(data) { }
    }
}