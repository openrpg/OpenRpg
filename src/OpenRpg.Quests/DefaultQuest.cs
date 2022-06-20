using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests
{
    public class DefaultQuest : IQuest
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public bool IsRepeatable { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IEnumerable<Objective> Objectives { get; set; } = Array.Empty<Objective>();
        public IEnumerable<Reward> Rewards { get; set; } = Array.Empty<Reward>();
        public IEnumerable<Reward> Gifts { get; set; } = Array.Empty<Reward>();
        public IQuestVariables Variables { get; set; } = new DefaultQuestVariables();
    }
}