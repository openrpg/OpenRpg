using System;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Scaling;

namespace OpenRpg.Items.TradeSkills.Calculator
{
    public class TradeSkillCalculator : ITradeSkillCalculator
    {
        public float MinimumPointThreshold { get; set; } = 0.5f;
        public float PointMultiplier { get; set; } = 1.0f;
        public float MaximumSkillDifference { get; set; } = 10.0f;
    
        public IScalingFunction SkillPointCurve { get; }

        public TradeSkillCalculator()
        {
            SkillPointCurve = new ScalingFunction(PresetCurves.InverseLinear, 0, 1, 0, MaximumSkillDifference);
        }

        public bool CanUseSkill(int skillScore, int skillDifficulty)
        {
            var skillDifference = skillDifficulty - skillScore;
            var absoluteScore = Math.Abs(skillDifference);
            return absoluteScore <= MaximumSkillDifference;
        }
    
        public int CalculateSkillUpPointsFor(int skillScore, int skillDifficulty)
        {
            var skillDifference = skillDifficulty - skillScore;
            var absoluteScore = Math.Abs(skillDifference);
            if (absoluteScore > MaximumSkillDifference) { return 0; }

            var result = SkillPointCurve.Plot(absoluteScore);
            if (result < MinimumPointThreshold) { return 0; }
            return (int)Math.Round(result * PointMultiplier);
        }
    }
}