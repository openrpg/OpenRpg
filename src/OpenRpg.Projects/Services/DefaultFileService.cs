namespace OpenRpg.Projects.Services;

public class DefaultFileService : IFileService
{
    public Task<string> GetContents(string file)
    { return File.ReadAllTextAsync(file); }

    public async Task<IReadOnlyCollection<string>> GetChildContents(string fileOrDirectory, string filter)
    { return Directory.GetFiles(fileOrDirectory, filter); }

    public async Task<bool> Exists(string file)
    {
        var attriutes = File.GetAttributes(file);
        return attriutes.HasFlag(FileAttributes.Directory) ? Directory.Exists(file) : File.Exists(file);
    }
}