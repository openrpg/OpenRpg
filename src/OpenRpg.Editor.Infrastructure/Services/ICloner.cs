namespace OpenRpg.Editor.Infrastructure.Services
{
    public interface ICloner
    {
        T Clone<T>(T source) where T : new();
    }
}