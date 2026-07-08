using OpenRpg.Genres.Scifi.Ships;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipTemplateVariableExtensions
    {
        public static ShipEquipmentSlots EquipmentSlots(this ShipTemplateVariables vars)
        {
            if (!vars.ContainsKey(ShipTemplateVariableTypes.EquipmentSlots))
            { vars.AddVariable(ShipTemplateVariableTypes.EquipmentSlots, new ShipEquipmentSlots()); }

            return vars[ShipTemplateVariableTypes.EquipmentSlots] as ShipEquipmentSlots;
        }
    }
}