using OpenRpg.Core.Extensions;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Entities.Procedural;
using OpenRpg.Entities.Types;
using OpenRpg.Tags;

namespace OpenRpg.Entities.Extensions
{
    public static class ITemplateVariableExtensions
    {
        public static bool HasProceduralEffects(this ITemplateVariables vars)
         => vars.ContainsKey(CoreTemplateVariableTypes.ProceduralEffects);
        
        public static ProceduralEffects ProceduralEffects(this ITemplateVariables vars)
            => vars.GetAsOrDefault(CoreTemplateVariableTypes.ProceduralEffects, () => new ProceduralEffects());
        
        public static void ProceduralEffects(this ITemplateVariables vars, ProceduralEffects proceduralEffects)
            => vars[CoreTemplateVariableTypes.ProceduralEffects] = proceduralEffects;
        
        public static bool HasAssetCode(this ITemplateVariables vars) 
            => vars.ContainsKey(CoreTemplateVariableTypes.AssetCode);
        
        public static string AssetCode(this ITemplateVariables vars) 
            => (string)vars[CoreTemplateVariableTypes.AssetCode];
        
        public static void AssetCode(this ITemplateVariables vars, string assetCode) 
            => vars[CoreTemplateVariableTypes.AssetCode] = assetCode;
        
        public static string Tags(this ITemplateVariables variables) => (string)variables[CoreTemplateVariableTypes.Tags];
        public static void Tags(this ITemplateVariables variables, TagList tags) => variables[CoreTemplateVariableTypes.Tags] = tags;

    }
}