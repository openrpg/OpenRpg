using System;
using System.Collections.Generic;
using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Cards.Types;
using OpenRpg.Combat.Abilities;
using OpenRpg.Core.Effects;

namespace OpenRpg.Cards.Genres
{
    public class AbilityCard : GenericDataCard<Ability>
    {
        public override int CardType => CardTypes.AbilityCard;
        public override IEnumerable<Effect> Effects { get; } = Array.Empty<Effect>();

        public AbilityCard(Ability ability) : base(ability)
        {}
    }
}