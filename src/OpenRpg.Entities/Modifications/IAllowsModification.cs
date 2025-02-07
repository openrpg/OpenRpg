using System.Collections.Generic;

namespace OpenRpg.Entities.Modifications
{
    public interface IAllowsModification
    {
        IReadOnlyCollection<ModificationAllowance> ModificationAllowances { get; }
    }
}