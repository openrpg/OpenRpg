using System.Collections.Generic;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Entity;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Genres.Extensions
{
    public static class IEntityExtensions
    {
        /// <summary>
        /// Gets all basic effects from known sources on an entity
        /// </summary>
        /// <param name="entity">The entity to process</param>
        /// <returns>All known effects that are applied to the entity</returns>
        /// <remarks>Multiclass are not included as that's likely to need complex mapping logic</remarks>
        public static IEnumerable<Effect> GetEffects(this IEntity entity)
        {
            var effects = new List<Effect>();
            
            if(entity.Variables.HasRace())
            { effects.AddRange(entity.Variables.Race().Effects);}
            
            if(entity.Variables.HasClass())
            { effects.AddRange(entity.Variables.Class().Template.Effects); }
            
            if(entity.Variables.HasEquipment())
            { effects.AddRange(entity.Variables.Equipment().GetEffects()); }
            
            if(entity.Variables.HasActiveEffects())
            { effects.AddRange(entity.Variables.ActiveEffects().Effects); }
            
            return effects;
        }
    }
}