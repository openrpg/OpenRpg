using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Modifications
{
    public class ModificationAllowance : IHasRequirements
    {
        public int AmountAllowed { get; set; }
        public int ModificationType { get; set; }
        
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}