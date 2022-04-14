namespace OpenRpg.Tags
{
    public readonly struct TagWeighting
    {
        public readonly int Tag;
        public readonly float Weight;

        public TagWeighting(int tag, float weight)
        {
            Tag = tag;
            Weight = weight;
        }
    }
}