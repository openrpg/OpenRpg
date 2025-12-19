using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Entities.Races.Variables;

namespace OpenRpg.Cards.Genres
{
    public class RaceCard : TemplateDataCardWithEffects<RaceTemplate, RaceTemplateVariables>
    {
        public override int CardType => CardTypes.RaceCard;

        public RaceCard(RaceTemplate data) : base(data) {}
    }
}