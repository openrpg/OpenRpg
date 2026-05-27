using OpenRpg.Core.Extensions;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Types;
using OpenRpg.Tags;

namespace OpenRpg.Entities.Extensions
{
    public static class ObjectVariableExtensions
    {
        public static bool HasAssetCode(this IVariables<object> vars)
            => vars.ContainsKey(CoreAnyVariableTypes.AssetCode);
        
        public static bool HasTags(this IVariables<object> vars)
            => vars.ContainsKey(CoreAnyVariableTypes.Tags);

        extension(IVariables<object> vars)
        {
            public string AssetCode 
            {
                get => vars.GetAsOrDefault(CoreAnyVariableTypes.AssetCode, () => string.Empty);
                set => vars[CoreAnyVariableTypes.AssetCode] = value;
            }
            
            public TagList Tags 
            {
                get => vars.GetAsOrDefaultAndSet(CoreAnyVariableTypes.Tags, () => new TagList());
                set => vars[CoreAnyVariableTypes.Tags] = value;
            }
        }
    }
}