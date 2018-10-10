using System.Collections.Generic;

namespace OpenRpg.Core.Modifications
{
    public interface IHasModifications
    {
        IEnumerable<IModification> Modifications { get; }
    }

    public interface IAllowsModification
    {
        IEnumerable<ModificationAllowance> ModificationAllowances { get; }
    }
}