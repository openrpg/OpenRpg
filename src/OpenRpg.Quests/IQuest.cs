using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Quests
{
    public interface IQuest : IHasDataId, IHasRequirements, IHasLocaleDescription
    {
        bool IsRepeatable { get; }
        IEnumerable<Objective> Objectives { get; }
        IEnumerable<Reward> Rewards { get; }
        IEnumerable<Reward> Gifts { get; }
        IQuestVariables Variables { get; }
    }
}