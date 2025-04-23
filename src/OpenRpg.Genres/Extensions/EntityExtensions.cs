using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Entity;
using OpenRpg.Entities.Extensions;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Genres.Extensions
{
    public static class EntityExtensions
    {
        public static IReadOnlyCollection<IEffect> GetEffects(this Entity entity, ITemplateAccessor templateAccessor)
        {
            var effects = new List<IEffect>();

            if (entity.Variables.HasRace()) { effects.AddRange(entity.Variables.Race().GetEffects(templateAccessor)); }
            if (entity.Variables.HasClass()) { effects.AddRange(entity.Variables.Class().GetEffects(templateAccessor)); }
            if (entity.Variables.HasEquipment()) { effects.AddRange(entity.Variables.Equipment().GetEffects(templateAccessor)); }
            return effects;
        }
        
        public static float GetHealthPercentage(this Entity entity)
        { return (float)entity.State.Health() / entity.Stats.MaxHealth(); }
        
        public static float GetStaminaPercentage(this Entity entity)
        { return (float)entity.State.Stamina() / entity.Stats.MaxStamina(); }
    }
}