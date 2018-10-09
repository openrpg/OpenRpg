namespace OpenRpg.Core.Requirements
{
    /// <summary>
    /// Represents a requirement that must be met 
    /// </summary>
    public class Requirement
    {
        /// <summary>
        /// The type of requirement that needs to be met
        /// </summary>
        public int Type { get; }
        
        /// <summary>
        /// The required value for the given type
        /// i.e Requires Level > 5, with the requirement type indicating level, and the value being 5
        /// </summary>
        /// <remarks>The value is an int so it can be used for ids, as well as numeric values</remarks>
        public int Value { get; }


        public Requirement(int type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}