using Xunit;

namespace OpenRpg.UnitTests.Crafting;

public class TradeSkillCalculatorTests
{
    [Theory]
    [InlineData(1, 1, 1, true)]
    [InlineData(0, 1, 1, true)]
    [InlineData(0, 2, 1, false)]
    [InlineData(0, 2, 2, true)]
    [InlineData(10, 100, 10, false)]
    [InlineData(10, 100, 80, false)]
    [InlineData(10, 100, 90, true)]
    public void should_correctly_work_out_skill_use(int skillScore, int skillDifficulty, int maxSkillDifference, bool shouldBeAbleToUse)
    {
        var tradeSkillCalculator = new TradeSkillCalculator() { MaximumSkillDifference = maxSkillDifference };
        var canUse = tradeSkillCalculator.CanUseSkill(skillScore, skillDifficulty);
        Assert.Equal(shouldBeAbleToUse, canUse);
    }
    
    [Theory]
    [InlineData(1, 1, 10, 0.5f, 1, 1)]
    [InlineData(1, 1, 10, 0.5f, 3, 3)]
    [InlineData(1, 10, 10, 0.5f, 1, 0)]
    [InlineData(1, 10, 10, 0.1f, 5, 1)]
    [InlineData(1, 5, 10, 0.1f, 5, 3)]
    public void should_correctly_work_out_skill_up_points(int skillScore, int skillDifficulty, int maxSkillDifference, float minimumPointThreshold, float pointMultiplier, int expectedPoints)
    {
        var tradeSkillCalculator = new TradeSkillCalculator()
        {
            MinimumPointThreshold = minimumPointThreshold,
            PointMultiplier = pointMultiplier,
            MaximumSkillDifference = maxSkillDifference
        };
        var actualPoints = tradeSkillCalculator.CalculateSkillUpPointsFor(skillScore, skillDifficulty);
        Assert.Equal(expectedPoints, actualPoints);
    }
}