using System.Collections.Generic;
using OpenRpg.Cards.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Cards.Variables
{
    public class DefaultCardVariables : ObjectVariables, ICardVariables
    {
        public DefaultCardVariables(IDictionary<int, object> internalVariables = null) : base(ICardCoreVariableTypes.CardVariables, internalVariables)
        {
        }
    }
}