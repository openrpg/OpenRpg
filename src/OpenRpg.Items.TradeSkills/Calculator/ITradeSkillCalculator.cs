namespace OpenRpg.Items.TradeSkills.Calculator
{
    public interface ITradeSkillCalculator
    {
        float MinimumPointThreshold { get; set; }
        float PointMultiplier { get; set; }
        float MaximumSkillDifference { get; set; }
        
        bool CanUseSkill(int skillScore, int skillDifficulty);
        int CalculateSkillUpPointsFor(int skillScore, int skillDifficulty);
    }
}