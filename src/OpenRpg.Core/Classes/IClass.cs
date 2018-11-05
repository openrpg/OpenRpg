namespace OpenRpg.Core.Classes
{
    public interface IClass
    {
        int Level { get; }
        IClassTemplate ClassTemplate { get; }
    }
}