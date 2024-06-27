using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Races;
using OpenRpg.Core.Races.Templates;

namespace OpenRpg.Cards.Genres
{
    public class RaceCard : GenericDataCardWithEffects<IRaceTemplate>
    {
        public override int CardType => CardTypes.RaceCard;

        public RaceCard(IRaceTemplate data) : base(data) {}
    }
}