namespace OpenRpg.Quests.Objectives
{
    public interface IObjectiveChecker<in T>
    {
        bool IsObjectiveMet(T target, Objective objective);
    }
}
