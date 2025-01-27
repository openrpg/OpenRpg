using System.Collections.Generic;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Variables;

namespace OpenRpg.AdviceEngine.Variables
{
    public interface IUtilityVariables : IKeyedVariables<UtilityKey, float>
    {
        IReadOnlyCollection<KeyValuePair<UtilityKey, float>> GetRelatedUtilities(int utilityId);
        void Remove(int utilityId);
        bool ContainsKey(int utilityId);

        float this[int utilityId] { get; set; }
    }
}