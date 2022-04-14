namespace OpenRpg.Tags
{
    public class TaggedWithScore
    {
        public ITagged Entity { get; }
        public float Score { get; }

        public TaggedWithScore(ITagged entity, float score)
        {
            Entity = entity;
            Score = score;
        }
    }
}