using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Modifications
{
    public class ModificationAllowance : IHasRequirements
    {
        public int AmountAllowed { get; set; }
        public int ModificationType { get; set; }
        
        public IEnumerable<Requirement> Requirements { get; set; } = new Requirement[0];
    }
}