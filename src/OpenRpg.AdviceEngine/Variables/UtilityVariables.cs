using System.Collections.Generic;
using System.Linq;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.AdviceEngine.Variables
{
    public class UtilityVariables : KeyedVariables<UtilityKey, float>, IUtilityVariables
    {
        public UtilityVariables(IDictionary<UtilityKey, float> internalVariables = null) : base(AdviceEngineVariableTypes.UtilityVariables, internalVariables)
        {}
        
        public IReadOnlyCollection<KeyValuePair<UtilityKey, float>> GetRelatedUtilities(int utilityId)
        { return InternalVariables.Where(x => x.Key.UtilityId == utilityId).ToArray(); }

        public void Remove(int utilityId)
        { Remove(new UtilityKey(utilityId)); }

        public bool ContainsKey(int utilityId)
        { return ContainsKey(new UtilityKey(utilityId)); }
        
        public float this[int utilityId]
        {
            get => InternalVariables[new UtilityKey(utilityId)];
            set => this[new UtilityKey(utilityId)] = value;
        }
    }
}