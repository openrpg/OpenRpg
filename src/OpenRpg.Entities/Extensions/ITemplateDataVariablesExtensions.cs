using System;
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

        extension(ITemplateDataVariables vars)
        {
            public int Level
            {
                get => vars.GetIntOrDefault(CoreTemplateDataVariableTypes.Level, 1);
                set => vars[CoreTemplateDataVariableTypes.Level] = value;
            }
        }
        
        public static bool HasProceduralAssociation(this ITemplateDataVariables vars)
            => vars.ContainsKey(CoreTemplateDataVariableTypes.ProceduralAssociations);
        
        extension(ITemplateDataVariables vars)
        {
            public IReadOnlyCollection<Association> ProceduralAssociation
            {
                get => vars.GetAsOrDefault(CoreTemplateDataVariableTypes.ProceduralAssociations, IReadOnlyCollection<Association>.Empty);
                set => vars[CoreTemplateDataVariableTypes.ProceduralAssociations] = value;
            }
        }
    }
}