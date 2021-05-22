using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Quests
{
    public class DefaultQuest : IQuest
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public bool IsRepeatable { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; } = new Requirement[0];
        public IEnumerable<Objective> Objectives { get; set; } = new Objective[0];
        public IEnumerable<Reward> Rewards { get; set; } = new Reward[0];
        public IEnumerable<Reward> Gifts { get; set; } = new Reward[0];
        public IQuestVariables Variables { get; set; } = new DefaultQuestVariables();
    }
}