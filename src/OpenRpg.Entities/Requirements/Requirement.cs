using OpenRpg.Core.Common;
using OpenRpg.Core.Utils;

namespace OpenRpg.Entities.Requirements
{
    /// <summary>
    /// Represents a requirement that must be met 
    /// </summary>
    public class Requirement
    {
        /// <summary>
        /// The type of requirement that needs to be met
        /// </summary>
        public int RequirementType { get; set; }

        /// <summary>
        /// The associated data for the requirement
        /// i.e Requires item id 1, Requires Level > 5
        /// </summary>
        /// <remarks>This may be empty in some cases depending on requirement type</remarks>
        public Association Association { get; set; }
    }
}