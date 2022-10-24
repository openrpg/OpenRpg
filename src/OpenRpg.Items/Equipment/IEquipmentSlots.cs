using OpenRpg.Core.Variables;

namespace OpenRpg.Items.Equipment
{
    /// <summary>
    /// Equipment slots are keyed on the slot type, i.e Left Hand, Chest, Ring1, Ring2 etc, with the internal slot
    /// defining what items can be slotted in there i.e Sword, Hammer, Ring etc
    /// </summary>
    /// <remarks>
    /// There is a difference on a SLOT TYPE (what the slot is) and the ITEM TYPE which can be configured per slot
    /// to indicate if it can be equipped, so you may have a single slot for LeftHand but you may have multiple
    /// item types that are weapons i.e Sword, Shield etc or you may just have a singular Weapon type.
    /// </remarks>
    public interface IEquipmentSlots : IVariables<IEquipmentSlot>
    {
        
    }
}