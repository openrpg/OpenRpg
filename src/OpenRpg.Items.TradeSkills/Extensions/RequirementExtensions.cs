using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Extensions;

public static class RequirementExtensions
{
    extension(IReadOnlyCollection<Requirement> requirements)
    {
        public bool HasTradeSkillRequirement()
        { return requirements.Any(x => x.RequirementType == TradeSkillRequirementTypes.TradeSkillRequirement); }

        public bool HasTradeSkillRequirement(int tradeSkillType)
        { return requirements.Any(x => x.RequirementType == TradeSkillRequirementTypes.TradeSkillRequirement && x.Association.AssociatedId == tradeSkillType); }

        public IReadOnlyCollection<Requirement> GetTradeSkillRequirements()
        {
            return requirements
                .Where(x => x.RequirementType == TradeSkillRequirementTypes.TradeSkillRequirement)
                .ToArray();
        }

        public Requirement GetTradeSkillRequirement(int tradeSkillType)
        {
            return requirements
                .FirstOrDefault(x =>
                    x.RequirementType == TradeSkillRequirementTypes.TradeSkillRequirement &&
                    x.Association.AssociatedId == tradeSkillType);
        }
        
        public int GetTradeSkillRequirementValue(int tradeSkillType)
        { return requirements.GetTradeSkillRequirement(tradeSkillType)?.Association.AssociatedValue ?? 0; }
    }
}