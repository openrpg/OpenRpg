using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Templates
{
    /// <summary>
    /// The ITemplateData interface indicates its an instance of a template with its own data unique to this instance
    /// of the template.
    /// </summary>
    public interface ITemplateData: IHasTemplateLink
    {
        
    }
    
    public interface ITemplateData<out T>: ITemplateData, IHasVariables<T> where T : IVariables<object>
    {
        
    }
}