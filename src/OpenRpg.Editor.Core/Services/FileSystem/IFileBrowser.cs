namespace OpenRpg.Editor.Core.Services.FileSystem;

public interface IFileBrowser
{
    string BrowseToOpenFile(string? startingDirectory = null, string? filterList = null);
    string BrowseToSaveFile(string? startingDirectory = null, string? filterList = null);
    string BrowseToFolder(string? startingDirectory = null);
}