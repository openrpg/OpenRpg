using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IAllowsModification
    {
        IEnumerable<ModificationAllowance> ModificationAllowances { get; }
    }
}