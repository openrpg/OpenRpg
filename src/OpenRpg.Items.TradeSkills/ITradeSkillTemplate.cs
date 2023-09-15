using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public interface ITradeSkillTemplate : IHasDataId, IHasRequirements, IHasVariables<ITradeSkillTemplateVariables>
    {
        /// <summary>
        /// Time in seconds for the action to complete i.e gathered/created
        /// </summary>
        public float TimeToComplete { get; }

        /// <summary>
        /// The category of trade skill type
        /// </summary>
        public int SkillType { get; }
    
        /// <summary>
        /// Indicates how difficult this is to get, effects gather/creation rates and level up rates
        /// </summary>
        public int SkillDifficulty { get; }
    }
}