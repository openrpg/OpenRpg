using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates.Variables;
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
        
        public static bool HasPatternId(this ITemplateVariables vars)
            => vars.ContainsKey(CoreTemplateVariableTypes.PatternId);
        
        public static bool HasPatternTypeId(this ITemplateVariables vars)
            => vars.ContainsKey(CoreTemplateVariableTypes.PatternTypeId);

        extension(ITemplateVariables vars)
        {
            public IReadOnlyCollection<IEffect> Effects
            {
                get => vars.GetAsOrDefaultAndSet(CoreTemplateVariableTypes.Effects, () => new List<IEffect>());
                set => vars[CoreTemplateVariableTypes.Effects] = value;
            }

            public IReadOnlyCollection<Requirement> Requirements
            {
                get => vars.GetAsOrDefaultAndSet(CoreTemplateVariableTypes.Requirements, () => new List<Requirement>());
                set => vars[CoreTemplateVariableTypes.Requirements] = value;
            }
            
            public ProceduralEffects ProceduralEffects
            {
                get => vars.GetAsOrDefault(CoreTemplateVariableTypes.ProceduralEffects, () => new ProceduralEffects());
                set => vars[CoreTemplateVariableTypes.ProceduralEffects] = value;
            }
            
            public int PatternId
            {
                get => vars.GetInt(CoreTemplateVariableTypes.PatternId);
                set => vars[CoreTemplateVariableTypes.PatternId] = value;
            }
            
            public int PatternTypeId
            {
                get => vars.GetInt(CoreTemplateVariableTypes.PatternTypeId);
                set => vars[CoreTemplateVariableTypes.PatternTypeId] = value;
            }
        }
    }
}