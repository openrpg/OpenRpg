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
        public static IReadOnlyCollection<IEffect> GetEffects(this EntityData entityData, ITemplateAccessor templateAccessor)
        {
            var effects = new List<IEffect>();

            if (entityData.Variables.HasRace()) { effects.AddRange(entityData.Variables.Race.GetEffects(templateAccessor)); }
            if (entityData.Variables.HasClass()) { effects.AddRange(entityData.Variables.Class.GetEffects(templateAccessor)); }
            if (entityData.Variables.HasEquipment()) { effects.AddRange(entityData.Variables.Equipment.GetEffects(templateAccessor)); }
            return effects;
        }
        
        extension(EntityData entityData)
        {
            public float HealthPercentage => (float)entityData.State.Health / entityData.Stats.MaxHealth;
            public float StaminaPercentage => (float)entityData.State.Stamina / entityData.Stats.MaxStamina;
        }
    }
}