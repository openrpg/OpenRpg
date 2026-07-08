using System;
using OpenRpg.Core.Utils;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Scaling;

namespace OpenRpg.Items.TradeSkills.Calculator
{
    /// <summary>
    /// This is a default extensible calculator that can be used for most common use cases
    /// </summary>
    public class TradeSkillCalculator : ITradeSkillCalculator
    {
        /// <summary>
        /// This is the gated minimum threshold of the 0-1 plot check, defaults to 0.1f
        /// </summary>
        public float MinimumPointThreshold { get; set; } = 0.1f;
        
        /// <summary>
        /// This is the multiplier added to the resulting value post randomness calculations, defaults to 1.0f
        /// </summary>
        public float PointMultiplier { get; set; } = 1.0f;
        
        /// <summary>
        /// This is the threshold for skill calculating skill points, defaults to 10.0f
        /// </summary>
        public float MaximumSkillDifference { get; set; } = 10.0f;
        
        /// <summary>
        /// This is the additional randomness applied, defaults to 0.0f
        /// </summary>
        /// <remarks>The variance is applied negatively for low bound and positively for high bound</remarks>
        public float RandomnessVariance { get; set; } = 0.0f;
    
        public IScalingFunction SkillPointCurve { get; }
        public IRandomizer Randomizer { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="randomizer">The randomizer to use</param>
        /// <param name="curveFunction">The optional curve function to apply, by default uses inverse linear</param>
        public TradeSkillCalculator(IRandomizer randomizer, ICurveFunction curveFunction = null)
        {
            SkillPointCurve = new ScalingFunction(curveFunction ?? PresetCurves.InverseLinear, new RangeF(0, 1), new RangeF(0, MaximumSkillDifference));
            Randomizer = randomizer;
        }
    
        public int CalculateSkillUpPointsFor(int skillScore, int skillDifficulty)
        {
            var skillDifference = skillDifficulty - skillScore;
            var absoluteScore = Math.Abs(skillDifference);
            if (absoluteScore > MaximumSkillDifference) { return 0; }

            var result = SkillPointCurve.Plot(absoluteScore);
            var randomVariance = Randomizer.Random(-RandomnessVariance, RandomnessVariance);
            var totalResult = result + randomVariance;
            if (totalResult <= MinimumPointThreshold) { return 0; }
            return (int)Math.Ceiling(totalResult * PointMultiplier);
        }
    }
}