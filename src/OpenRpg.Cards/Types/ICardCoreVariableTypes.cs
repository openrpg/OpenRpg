using OpenRpg.Combat.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Cards.Types
{
    public interface ICardCoreVariableTypes : ItemCoreVariableTypes, CombatVariableTypes
    {
        public static int CardVariables = 50;
    }
}