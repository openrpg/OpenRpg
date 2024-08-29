using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests
{
    public class Quest : ITemplate, IHasRequirements, IHasVariables<QuestVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public bool IsRepeatable { get; set; }
        
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IReadOnlyCollection<Objective> Objectives { get; set; } = Array.Empty<Objective>();
        public IReadOnlyCollection<Reward> Rewards { get; set; } = Array.Empty<Reward>();
        public IReadOnlyCollection<Reward> Gifts { get; set; } = Array.Empty<Reward>();
        public QuestVariables Variables { get; set; } = new QuestVariables();
    }
}