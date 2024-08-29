using System;

namespace OpenRpg.AdviceEngine.Keys
{
    public struct UtilityKey : IEquatable<UtilityKey>
    {
        public readonly int UtilityId;
        public readonly int RelatedId;

        public UtilityKey(int utilityId, int relatedId = 0)
        {
            UtilityId = utilityId;
            RelatedId = relatedId;
        }

        public bool Equals(UtilityKey other)
        {
            return UtilityId == other.UtilityId && RelatedId == other.RelatedId;
        }

        public override bool Equals(object obj)
        {
            return obj is UtilityKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (UtilityId * 397) ^ RelatedId;
            }
        }
    }
}