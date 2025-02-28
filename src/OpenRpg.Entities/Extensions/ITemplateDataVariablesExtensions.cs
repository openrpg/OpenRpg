using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateDataVariablesExtensions
    {
        public static bool HasLevel(this ITemplateDataVariables vars)
            => vars.ContainsKey(CoreTemplateDataVariableTypes.Level);
        
        public static int Level(this ITemplateDataVariables vars)
            => vars.GetIntOrDefault(CoreTemplateDataVariableTypes.Level, 1);
        
        public static void Level(this ITemplateDataVariables vars, int level)
            => vars[CoreTemplateDataVariableTypes.Level] = level;
        
        public static bool HasProceduralAssociation(this ITemplateDataVariables vars)
            => vars.ContainsKey(CoreTemplateDataVariableTypes.ProceduralAssociations);
        
        public static IReadOnlyCollection<Association> ProceduralAssociation(this ITemplateDataVariables vars)
            => vars.GetAsOrDefault(CoreTemplateDataVariableTypes.ProceduralAssociations, () => new List<Association>());
        
        public static void ProceduralAssociation(this ITemplateDataVariables vars, IReadOnlyCollection<Association> effectAssociations)
            => vars[CoreTemplateDataVariableTypes.ProceduralAssociations] = effectAssociations;

    }
}