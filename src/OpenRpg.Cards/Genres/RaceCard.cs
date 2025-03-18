using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Entities.Races.Templates;

namespace OpenRpg.Cards.Genres
{
    public class RaceCard : GenericDataCardWithEffects<RaceTemplate>
    {
        public override int CardType => CardTypes.RaceCard;

        public RaceCard(RaceTemplate data) : base(data) {}
    }
}