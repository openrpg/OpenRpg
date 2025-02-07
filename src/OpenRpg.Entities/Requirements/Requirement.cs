using OpenRpg.Core.Common;

namespace OpenRpg.Entities.Requirements
{
    /// <summary>
    /// Represents a requirement that must be met 
    /// </summary>
    public class Requirement : IHasAssociation
    {
        /// <summary>
        /// The type of requirement that needs to be met
        /// </summary>
        public int RequirementType { get; set; }
        
        /// <summary>
        /// The associated id for the requirement
        /// i.e Requires item id 1
        /// </summary>
        /// <remarks>This may be empty in some cases depending on requirement type</remarks>
        public int AssociatedId { get; set; }
        
        /// <summary>
        /// The associated value for the associated type (or requirement type)
        /// i.e Requires Level > 5, with the requirement type indicating level, and the value being 5
        /// </summary>
        public int AssociatedValue { get; set; }
    }
}