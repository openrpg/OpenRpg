using OpenRpg.Combat.Types;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Quests.Types;

namespace OpenRpg.Genres.Types
{
    public interface GenreRequirementTypes : TradeSkillRequirementTypes, CombatRequirementTypes, QuestRequirementTypes
    {
        public static readonly int MaxHealthRequirement = 100;
        public static readonly int MaxStaminaRequirement = 101;
        public static readonly int MovementSpeedRequirement = 103;

    }
}