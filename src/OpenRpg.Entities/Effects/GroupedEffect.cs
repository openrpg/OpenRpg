namespace OpenRpg.Entities.Effects
{
    /// <summary>
    /// Same as a scaled effect but indicates how it should be grouped when being factored into procedural scenarios
    /// </summary>
    public class GroupedEffect : ScaledEffect
    {
        /// <summary>
        /// The grouping type to apply this effect to i.e Primary, Optional or other custom types
        /// </summary>
        public int GroupType { get; set; } = 0;
    }
}