using OpenRpg.Core.Variables;
using OpenRpg.Editor.Infrastructure.Variables;

namespace OpenRpg.Editor.Infrastructure.Extensions
{
    public static class CustomVariableExtensions
    {
        public static string AssetCode(this IVariables<object> variables) => (string)variables[CustomVariableTypes.AssetCode];
        public static void AssetCode(this IVariables<object> variables, string assetCode) => variables[CustomVariableTypes.AssetCode] = assetCode;
    }
}