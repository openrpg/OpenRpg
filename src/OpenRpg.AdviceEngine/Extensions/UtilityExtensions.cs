using System.Collections.Generic;
using System.Linq;
using OpenRpg.AdviceEngine.Keys;

namespace OpenRpg.AdviceEngine.Extensions
{
    public static class UtilityExtensions
    {
        public static float CalculateScore(this IReadOnlyCollection<KeyValuePair<UtilityKey, float>> variableUtilities)
        { return CalculateScore(variableUtilities.Select(x => x.Value).ToArray()); }

        public static float CalculateScore(this IReadOnlyCollection<float> utilities)
        {
            var score = 1.0f;
            var compensationFactor = (float)(1.0 - 1.0 / utilities.Count);
            foreach (var utility in utilities)
            {
                var modification = (float)((1.0 - utility) * compensationFactor);
                var scaledUtility = utility + (modification * utility);
                score *= scaledUtility;
            }
            return score;
        }
        
        public static float CalculateScore(params float[] utilities)
        {
            var score = 1.0f;
            var compensationFactor = (float)(1.0 - 1.0 / utilities.Length);
            foreach (var utility in utilities)
            {
                var modification = (float)((1.0 - utility) * compensationFactor);
                var scaledUtility = utility + (modification * utility);
                score *= scaledUtility;
            }
            return score;
        }
    }
}