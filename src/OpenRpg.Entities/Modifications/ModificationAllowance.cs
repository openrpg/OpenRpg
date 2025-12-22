using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Entities.Modifications
{
    public class ModificationAllowance
    {
        public int AmountAllowed { get; set; }
        public int ModificationType { get; set; }
        
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}