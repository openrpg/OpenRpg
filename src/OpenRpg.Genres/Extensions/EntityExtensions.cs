using System.Collections.Generic;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Entity;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Gets all basic effects from known sources on an entity
        /// </summary>
        /// <param name="entity">The entity to process</param>
        /// <returns>All known effects that are applied to the entity</returns>
        /// <remarks>Multiclass are not included as that's likely to need complex mapping logic</remarks>
        public static IReadOnlyCollection<Effect> GetEffects(this Entity entity, ITemplateAccessor templateAccessor)
        {
            var effects = new List<Effect>();

            if (entity.Variables.HasRace())
            {
                var race = entity.Variables.Race();
                var raceTemplate = templateAccessor.GetRaceTemplate(race.TemplateId);
                effects.AddRange(raceTemplate.Effects);
            }

            if (entity.Variables.HasClass())
            {
                var classData = entity.Variables.Class();
                var classTemplate = templateAccessor.GetClassTemplate(classData.TemplateId);
                effects.AddRange(classTemplate.Effects);
            }
            
            if(entity.Variables.HasEquipment())
            { effects.AddRange(entity.Variables.Equipment().GetEffects(templateAccessor)); }
            
            if(entity.Variables.HasActiveEffects())
            { effects.AddRange(entity.Variables.ActiveEffects().Effects); }
            
            return effects;
        }
    }
}