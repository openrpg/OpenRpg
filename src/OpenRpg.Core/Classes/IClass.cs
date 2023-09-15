using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes
{
    public interface IClass : IHasVariables<IClassVariables>
    {
        int Level { get; set; }
        IClassTemplate ClassTemplate { get; }
    }
}