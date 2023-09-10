using System.Collections.Generic;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Cards.Variables
{
    public class DefaultCardVariables : DefaultVariables<object>, ICardVariables
    {
        public DefaultCardVariables(IDictionary<int, object> internalVariables = null) : base(ICardCoreVariableTypes.CardVariables, internalVariables)
        {
        }
    }
}