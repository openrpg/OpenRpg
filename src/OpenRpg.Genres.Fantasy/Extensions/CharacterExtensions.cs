using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class CharacterExtensions
    {
        public static IEnumerable<Effect> GetActiveEffects(this ICharacter character)
        {
            var effects = new List<Effect>();
            effects.AddRange(character.Race.Effects);
            effects.AddRange(character.Class.ClassTemplate.Effects);
            effects.AddRange(character.Equipment.GetEquipmentEffects());
            return effects;
        }
    }
}