namespace OpenRpg.Tags
{
    /// <summary>
    /// This is a scored set of tags with some context against a given tag source
    /// </summary>
    public class ScoredTags
    {
        public ContextualTags ContextualTags { get; }
        public float Score { get; }

        public ScoredTags(ContextualTags contextualTags, float score)
        {
            ContextualTags = contextualTags;
            Score = score;
        }
    }
}