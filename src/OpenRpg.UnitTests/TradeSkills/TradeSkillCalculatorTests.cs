using Moq;
using OpenRpg.Core.Utils;
using OpenRpg.Items.TradeSkills.Calculator;
using Xunit;

namespace OpenRpg.UnitTests.TradeSkills;

public class TradeSkillCalculatorTests
{
    [Theory]
    [InlineData(1, 1, 10, 0.5f, 1, 1)]
    [InlineData(1, 1, 10, 0.5f, 3, 3)]
    [InlineData(1, 10, 10, 0.5f, 1, 0)]
    [InlineData(1, 10, 10, 0.1f, 5, 1)]
    [InlineData(1, 5, 10, 0.1f, 5, 3)]
    public void should_correctly_work_out_skill_up_points(int skillScore, int skillDifficulty, int maxSkillDifference, float minimumPointThreshold, float pointMultiplier, int expectedPoints)
    {
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(It.IsAny<float>(), It.IsAny<float>())).Returns(0.0f);
        var tradeSkillCalculator = new TradeSkillCalculator(mockRandomizer.Object)
        {
            MinimumPointThreshold = minimumPointThreshold,
            PointMultiplier = pointMultiplier,
            MaximumSkillDifference = maxSkillDifference
        };
        var actualPoints = tradeSkillCalculator.CalculateSkillUpPointsFor(skillScore, skillDifficulty);
        Assert.Equal(expectedPoints, actualPoints);
    }

    [Fact]
    public void should_factor_in_random_variance()
    {
        var mockRandomizer = new Mock<IRandomizer>();
        mockRandomizer.Setup(x => x.Random(It.IsAny<float>(), It.IsAny<float>())).Returns(0.1f);
        var tradeSkillCalculator = new TradeSkillCalculator(mockRandomizer.Object);
        var actualPoints = tradeSkillCalculator.CalculateSkillUpPointsFor(10, 15);
        Assert.Equal(1, actualPoints);
    }
}