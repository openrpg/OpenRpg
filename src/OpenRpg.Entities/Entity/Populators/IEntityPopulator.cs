namespace OpenRpg.Entities.Entity.Populators
{
    public interface IEntityPopulator<in T> where T : Entity
    {
        void Populate(T entity, bool refreshState = false);
    }
}