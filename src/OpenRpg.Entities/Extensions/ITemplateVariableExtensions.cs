using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Procedural.Effects;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateVariableExtensions
    {
        public static bool HasEffects(this ITemplateVariables vars)
         => vars.ContainsKey(CoreTemplateVariableTypes.Effects);
        
        public static bool HasRequirements(this ITemplateVariables vars)
            => vars.ContainsKey(CoreTemplateVariableTypes.Requirements);
        
        public static bool HasProceduralEffects(this ITemplateVariables vars)
            => vars.ContainsKey(CoreTemplateVariableTypes.ProceduralEffects);

        extension(ITemplateVariables vars)
        {
            public ProceduralEffects ProceduralEffects
            {
                get => vars.GetAsOrDefault(CoreTemplateVariableTypes.ProceduralEffects, () => new ProceduralEffects());
                set => vars[CoreTemplateVariableTypes.ProceduralEffects] = value;
            }

            public IReadOnlyCollection<IEffect> Effects
            {
                get => vars.GetAsOrDefault(CoreTemplateVariableTypes.Effects, IReadOnlyCollection<IEffect>.Empty);
                set => vars[CoreTemplateVariableTypes.Effects] = value;
            }

            public IReadOnlyCollection<Requirement> Requirements
            {
                get => vars.GetAsOrDefault(CoreTemplateVariableTypes.Requirements, IReadOnlyCollection<Requirement>.Empty);
                set => vars[CoreTemplateVariableTypes.Requirements] = value;
            }
        }
    }
}