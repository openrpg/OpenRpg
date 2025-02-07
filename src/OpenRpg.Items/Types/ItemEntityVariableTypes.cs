using OpenRpg.Entities.Types;

namespace OpenRpg.Items.Types
{
    public interface ItemEntityVariableTypes : EntityVariableTypes
    {
        public static readonly int Equipment = 10;
        public static readonly int Inventory = 11;
        public static readonly int LootTable = 12;
    }
}