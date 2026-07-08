using OpenRpg.Entities.Types;

namespace OpenRpg.Quests.Types;

public interface QuestRequirementTypes : CoreRequirementTypes
{
    public static readonly int TriggerRequirement = 60;
    public static readonly int QuestStateRequirement = 61;
    public static readonly int FactionStateRequirement = 62;
}