using OpenRpg.Combat.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Cards.Types
{
    public interface CardVariableTypes : ItemVariableTypes, CombatVariableTypes
    {
        public static int CardVariables = 50;
    }
}