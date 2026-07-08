namespace OpenRpg.Items.TradeSkills.Calculator
{
    public interface ITradeSkillCalculator
    {
        float MinimumPointThreshold { get; set; }
        float PointMultiplier { get; set; }
        float MaximumSkillDifference { get; set; }
        
        int CalculateSkillUpPointsFor(int skillScore, int skillDifficulty);
    }
}