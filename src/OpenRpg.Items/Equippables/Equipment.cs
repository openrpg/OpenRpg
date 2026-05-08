using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equippables
{
    public class Equipment : IHasVariables<EquipmentVariables>
    {
        public EquipmentSlots Slots { get; set; } = new();
        public EquipmentVariables Variables { get; set; } = new();
    }
}