namespace OpenRpg.Projects.Services;

public interface IFileService
{
    Task<string> GetContents(string file);
    Task<IReadOnlyCollection<string>> GetChildContents(string fileOrDirectory, string filter);
    Task<bool> Exists(string fileOrDirectory);
}