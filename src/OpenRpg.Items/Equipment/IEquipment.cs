using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equipment
{
    public interface IEquipment : IHasVariables<IEquipmentVariables>
    {
        IEquipmentSlots Slots { get; }
    }
}