namespace OpenRpg.Tags
{
    /// <summary>
    /// Represents a collection of tags associated with some form of context, be it a IDataTemplate, an id, etc
    /// </summary>
    /// <remarks>
    /// This is mainly intended for use when you want to do actions on multiple tagged entities at once and need
    /// to provide some form of context on the resulting object to indicate what owns it.
    /// </remarks>
    public class ContextualTags
    {
        public TagList Tags { get; set; }
        public object Context { get; set; }

        public ContextualTags(object context, TagList tags)
        {
            Tags = tags;
            Context = context;
        }
    }
}