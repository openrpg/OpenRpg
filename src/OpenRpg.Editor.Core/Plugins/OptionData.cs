namespace OpenRpg.Editor.Core.Plugins;

public record OptionData(int Id, string Name)
{
    public static OptionData Unknown(int id) => new(id, $"Unknown ({id})");
    
    public override string ToString() => $"{Name} ({Id})";
}