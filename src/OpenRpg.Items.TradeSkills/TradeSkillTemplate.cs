using System;
using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills
{
    public class TradeSkillTemplate : IHasDataId, IHasRequirements, IHasVariables<TradeSkillTemplateVariables>
    {
        /// <summary>
        /// The Id for this template
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gathering time in seconds, per unit gathered
        /// </summary>
        public float TimeToComplete { get; set; } = 1.0f;

        /// <summary>
        /// The category of skill type used for Gathering
        /// </summary>
        public int SkillType { get; set; }
    
        /// <summary>
        /// Indicates how difficult this is to get, effects if you can use the trade skill and skill up rates
        /// </summary>
        public int SkillDifficulty { get; set; }

        /// <summary>
        /// Requirements needed before this tradeskill is allowed
        /// </summary>
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();

        /// <summary>
        /// Variables for this template
        /// </summary>
        public TradeSkillTemplateVariables Variables { get; set; } = new TradeSkillTemplateVariables();
    }
}