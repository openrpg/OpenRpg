using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Classes.Variables;

namespace OpenRpg.Cards.Genres
{
    public class ClassCard : TemplateDataCardWithEffects<ClassTemplate, ClassTemplateVariables>
    {
        public override int CardType => CardTypes.ClassCard;

        public ClassCard(ClassTemplate data) : base(data) { }
    }
}