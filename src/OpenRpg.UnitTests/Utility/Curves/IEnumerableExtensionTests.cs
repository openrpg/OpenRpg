using System.Linq;
using OpenRpg.CurveFunctions.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Curves;

public class IEnumerableExtensionTests
{
    [Theory]
    [InlineData(1, new [] {0,1,2,3,4,5,6,7,8,9,10})]
    [InlineData(2, new [] {0,2,4,6,8,10})]
    [InlineData(3, new [] {0,3,6,9})]
    [InlineData(4, new [] {0,4,8})]
    [InlineData(5, new [] {0,5,10})]
    [InlineData(1000, new [] {0})]
    public void should_correctly_sample_elements_using_take_every(int sampleRate, int[] expected)
    {
        var baseRanges = Enumerable.Range(0, 11).ToArray();
        var sampledRange = baseRanges.TakeEvery(sampleRate).ToArray();
        Assert.Equal(expected.Length, sampledRange.Length);
        Assert.Equal(expected, sampledRange);
    }
}