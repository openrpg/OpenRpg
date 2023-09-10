using OpenRpg.Core.Types;

namespace OpenRpg.Items.Types
{
    public interface ItemEntityVariableTypes : EntityVariableTypes
    {
        public static readonly int Equipment = 1;
        public static readonly int Inventory = 2;
    }
}