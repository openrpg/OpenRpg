using OpenRpg.Core.Utils;

namespace OpenRpg.Quests
{
    public class Reward
    {
        public int RewardType { get; set; }
        public float RewardChance { get; set; }
        public Association Association { get; set; }
    }
}