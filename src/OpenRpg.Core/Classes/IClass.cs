using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes
{
    public interface IClass : IHasVariables<IClassVariables>, IHasTemplate<IClassTemplate>
    {
    }
}