using OpenRpg.Core.Requirements;

namespace OpenRpg.Entities.Requirements
{
    public interface IEntityRequirementChecker<in T> : IRequirementChecker<T> where T : Entity.Entity
    {
    }
}