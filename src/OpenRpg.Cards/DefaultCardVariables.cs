using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Cards
{
    public class DefaultCardVariables : DefaultVariables<object>, ICardVariables
    {
        public DefaultCardVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}