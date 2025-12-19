using System.Collections.Generic;
using OpenRpg.Cards.Effects;
using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Effects;

namespace OpenRpg.Cards.Genres
{
    public class EffectCard : GenericDataCard<CardEffects>
    {
        public override int CardType => CardTypes.EffectCard;
        
        public override IReadOnlyCollection<IEffect> Effects => Data.Effects;

        public EffectCard(CardEffects data) : base(data)
        {
        }
    }
}