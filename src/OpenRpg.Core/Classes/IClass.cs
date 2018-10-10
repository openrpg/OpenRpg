namespace OpenRpg.Core.Characters
{
    public interface IClass
    {
        int Level { get; }
        IClassTemplate ClassTemplate { get; }
    }
}