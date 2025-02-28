using OpenRpg.Core.Common;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Templates
{
    public interface ITemplate : IHasDataId, IHasLocaleDescription
    {
        
    }

    public interface ITemplate<out T> : ITemplate, IHasVariables<T> where T : ITemplateVariables
    {
        
    }
}