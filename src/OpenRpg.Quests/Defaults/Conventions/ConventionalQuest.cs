using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Quests.Defaults.Conventions
{
    public class ConventionalQuest : IConventionalQuest
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        
        public string NameLocaleId => $"{AssetCode}-name";
        public string DescriptionLocaleId => $"{AssetCode}-description";
        
        public bool IsRepeatable { get; set; }
        
        public IEnumerable<Requirement> Requirements { get; set; } = new List<Requirement>();
        public IEnumerable<Objective> Objectives { get; set; } = new List<Objective>();
        public IEnumerable<Reward> Rewards { get; set; } = new List<Reward>();
        public IEnumerable<Reward> Gifts { get; set; } = new List<Reward>();
    }
}