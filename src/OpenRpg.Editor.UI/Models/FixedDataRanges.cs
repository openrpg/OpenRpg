using System.Linq;

namespace OpenRpg.Editor.UI.Models;

public static class FixedDataRanges
{
    public static float[] ZeroToOneHundred = Enumerable.Range(0, 101)
        .Select(x => (float)x)
        .ToArray();

    public static float[] NormalizedZeroToOne = ZeroToOneHundred
        .Select(x => x / 100)
        .ToArray();

}