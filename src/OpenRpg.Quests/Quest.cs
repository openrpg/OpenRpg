using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Quests.Variables;

namespace OpenRpg.Quests
{
    public class Quest : ITemplate<QuestVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public bool IsRepeatable { get; set; }
        
        public IReadOnlyCollection<Objective> Objectives { get; set; } = [];
        public IReadOnlyCollection<Reward> Rewards { get; set; } = [];
        public IReadOnlyCollection<Reward> Gifts { get; set; } = [];
        
        public QuestVariables Variables { get; set; } = new();
    }
}