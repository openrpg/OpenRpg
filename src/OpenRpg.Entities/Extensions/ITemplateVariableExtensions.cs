using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateVariableExtensions
    {
        public static bool HasProceduralEffects(this ITemplateVariables vars)
            => vars.ContainsKey(CoreTemplateVariableTypes.ProceduralEffects);

        extension(ITemplateVariables vars)
        {
            public ProceduralEffects ProceduralEffects
            {
                get => vars.GetAsOrDefault(CoreTemplateVariableTypes.ProceduralEffects, () => new ProceduralEffects());
                set => vars[CoreTemplateVariableTypes.ProceduralEffects] = value;
            }
        }
    }
}