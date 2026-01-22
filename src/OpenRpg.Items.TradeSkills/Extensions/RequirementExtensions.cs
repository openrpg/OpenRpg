using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Extensions;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Extensions;

public static class RequirementExtensions
{
    public static bool HasTradeSkillRequirements(this IItemTradeSkillTemplateVariables vars)
    { return vars.HasRequirements() && vars.Requirements.HasTradeSkillRequirement(); }

    public static IReadOnlyCollection<Requirement> GetTradeSkillRequirements(this IItemTradeSkillTemplateVariables vars)
    {
        if (!vars.HasTradeSkillRequirements())
        { return []; }

        return vars.Requirements.GetTradeSkillRequirements();
    }
    
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