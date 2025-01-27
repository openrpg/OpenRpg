using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IAllowsModification
    {
        IReadOnlyCollection<ModificationAllowance> ModificationAllowances { get; }
    }
}