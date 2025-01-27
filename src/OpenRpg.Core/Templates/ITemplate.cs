using OpenRpg.Core.Common;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Templates
{
    public interface ITemplate : IHasDataId, IHasLocaleDescription
    {
        
    }

    public interface ITemplate<out T> : ITemplate, IHasVariables<T> where T : IVariables<object>
    {
        
    }
}