using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class IRandomizerExtensions
    {
        /// <summary>
        /// This is a quick way to check if you have made a critical hit
        /// </summary>
        /// <param name="randomizer">The randomizer to user for this</param>
        /// <param name="criticalChance">The critical chance between 0-1</param>
        /// <returns>true if a critical attack was made, false if not</returns>
        public static bool ShouldCritical(this IRandomizer randomizer, float criticalChance)
        {
            var sanitizedCritChance = criticalChance.SanitizeAndClamp();
            return randomizer.Random() <= sanitizedCritChance;
        }
    }
}