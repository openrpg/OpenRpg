using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Quests.Defaults
{
    public class DefaultQuest : IQuest
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public string AssetCode { get; set; }
        public bool IsRepeatable { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
        public IEnumerable<Objective> Objectives { get; set; }
        public IEnumerable<Reward> Rewards { get; set; }
        public IEnumerable<Reward> Gifts { get; set; }
        public IQuestVariables Variables { get; set; }
    }
}