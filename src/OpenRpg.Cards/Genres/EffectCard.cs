using OpenRpg.Cards.Effects;
using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;

namespace OpenRpg.Cards.Genres
{
    public class EffectCard : GenericDataCardWithEffects<CardEffects>
    {
        public override int CardType => CardTypes.EffectCard;

        public EffectCard(CardEffects data) : base(data)
        {
        }
    }
}