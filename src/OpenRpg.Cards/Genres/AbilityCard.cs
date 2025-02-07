using System;
using System.Collections.Generic;
using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Combat.Abilities;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Cards.Genres
{
    public class AbilityCard : GenericDataCard<Ability>
    {
        public override int CardType => CardTypes.AbilityCard;
        public override IReadOnlyCollection<StaticEffect> Effects { get; } = Array.Empty<StaticEffect>();

        public AbilityCard(Ability ability) : base(ability)
        {}
    }
}