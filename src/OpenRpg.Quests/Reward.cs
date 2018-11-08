using OpenRpg.Core.Common;

namespace OpenRpg.Quests
{
    public class Reward : IHasAssociation
    {
        public int RewardType { get; set; }
        public int AssociatedId { get; set; }
        public int AssociatedValue { get; set; }
        public float RewardChance { get; set; }
    }
}