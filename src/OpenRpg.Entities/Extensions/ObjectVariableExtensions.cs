using OpenRpg.Core.Variables;
using OpenRpg.Entities.Types;
using OpenRpg.Tags;

namespace OpenRpg.Entities.Extensions
{
    public static class ObjectVariableExtensions
    {
        public static bool HasAssetCode(this IVariables<object> vars)
            => vars.ContainsKey(CoreAnyVariableTypes.AssetCode);
        
        public static string AssetCode(this IVariables<object> vars) 
            => (string)vars[CoreAnyVariableTypes.AssetCode];
        
        public static void AssetCode(this IVariables<object> vars, string assetCode) 
            => vars[CoreAnyVariableTypes.AssetCode] = assetCode;
        
        public static string Tags(this IVariables<object> variables) => (string)variables[CoreAnyVariableTypes.Tags];
        public static void Tags(this IVariables<object> variables, TagList tags) => variables[CoreAnyVariableTypes.Tags] = tags;

    }
}