namespace OpenRpg.Core.Stats
{
    public class StatReference
    {
        public int StatType { get; }
        public float StatValue { get; }

        public StatReference(int statType, float statValue)
        {
            StatType = statType;
            StatValue = statValue;
        }
    }
}