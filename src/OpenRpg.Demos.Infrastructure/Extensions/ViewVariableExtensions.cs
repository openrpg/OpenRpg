using OpenRpg.Core.Variables;
using OpenRpg.Demos.Infrastructure.Types;

namespace OpenRpg.Demos.Infrastructure.Extensions
{
    public static class ViewVariableExtensions
    {
        public static string AssetCode(this IVariables<object> variables) => (string)variables[ViewVariableTypes.AssetCode];
        public static void AssetCode(this IVariables<object> variables, string assetCode) => variables[ViewVariableTypes.AssetCode] = assetCode;
    }
}