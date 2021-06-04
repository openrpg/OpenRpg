using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Items.Templates
{
    public class DefaultItemTemplateVariables : DefaultVariables<object>, IItemTemplateVariables
    {
        public DefaultItemTemplateVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}