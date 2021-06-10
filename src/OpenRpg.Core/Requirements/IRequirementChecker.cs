namespace OpenRpg.Core.Requirements
{
    public interface IRequirementChecker<in T>
    {
        bool IsRequirementMet(T target, Requirement requirement);
    }
}