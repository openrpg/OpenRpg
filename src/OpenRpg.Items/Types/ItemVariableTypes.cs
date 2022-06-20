using OpenRpg.Core.Types;

namespace OpenRpg.Items.Types
{
    public interface ItemVariableTypes : CoreVariableTypes
    {
        public static int ItemTemplateVariables = 20;
        public static int InventoryVariables = 21;
        public static int ItemVariables = 23;
        public static int EquipmentVariables = 24;
        public static int LootTableEntryVariables = 25;
        public static int EquipmentSlotVariables = 26;
    }
}