using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Services;

public interface ITemplateOptionsResolver
{
    OptionData[] GetOptionsForType(string typeSource, string templateTypeName = "");
}
