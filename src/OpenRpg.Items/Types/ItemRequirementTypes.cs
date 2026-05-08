using OpenRpg.Entities.Types;

namespace OpenRpg.Items.Types;

public interface ItemRequirementTypes : CoreRequirementTypes
{
    public static readonly int InventoryItemRequirement = 20;
    public static readonly int EquipmentItemRequirement = 21;
}