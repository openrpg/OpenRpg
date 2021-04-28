namespace OpenRpg.Core.Classes
{
    public interface IClass
    {
        int Level { get; set; }
        IClassTemplate ClassTemplate { get; }
        IClassVariables Variables { get; }
    }
}