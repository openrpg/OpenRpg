using OpenRpg.Core.Common;
using OpenRpg.Core.Utils;

namespace OpenRpg.Quests
{
    public class Reward : IHasAssociation
    {
        public int RewardType { get; set; }
        public Association Association { get; set; }
        public float RewardChance { get; set; }
    }
}