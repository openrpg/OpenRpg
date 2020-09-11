using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class ItemVariablesExtensions
    {
        public static int Amount(this IItemVariables variables) => (int)variables[ItemVariableTypes.Amount];
        public static void Amount(this IItemVariables variables, int value) => variables[ItemVariableTypes.Amount] = value;
    }
}