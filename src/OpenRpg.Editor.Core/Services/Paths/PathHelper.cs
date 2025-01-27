using System;
using System.IO;

namespace OpenRpg.Editor.Core.Services.Paths;

public class PathHelper
{
    /// <summary>
    /// The applications root path, this is where the exe lives
    /// </summary>
    public static string AppPath = Path.GetDirectoryName(Environment.ProcessPath) ?? Environment.CurrentDirectory;

    /// <summary>
    /// This is where all logs should be output to, based off the app path
    /// </summary>
    public static string LogPath = Path.GetFullPath($"{AppPath}/Logs/");
    
    /// <summary>
    /// This is where the plugins should reside, based off the app path
    /// </summary>
    public static string PluginPath = Path.GetFullPath($"{AppPath}/Plugins/");

}