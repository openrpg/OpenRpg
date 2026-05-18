using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables;

namespace OpenRpg.Items.Types
{
    public interface LootTableEntryVariableTypes
    {
        public static int Unknown = 0;

        public static int DropRate = 1;
        public static int IsUnique = 2;

        [CollectionElementType(typeof(Requirement))]
        public static int Requirements = 3;
    }
}