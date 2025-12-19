using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateVariableExtensions
    {
        public static bool HasEffects(this ITemplateVariables vars)
         => vars.ContainsKey(CoreTemplateVariableTypes.Effects);
        
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
        }
    }
}