using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Models;

public class LoadedProject
{
    public Project Project { get; set; } = new();
    public string ProjectPath { get; set; }
}