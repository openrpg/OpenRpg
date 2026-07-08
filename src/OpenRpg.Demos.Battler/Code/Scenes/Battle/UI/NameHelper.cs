using System.Text.RegularExpressions;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

public static class NameHelper
{
    public static string NormalizeName(string name)
    {
        return Regex.Replace(name, "(?<=[a-z])(?=[A-Z])", " ");
    }
}
