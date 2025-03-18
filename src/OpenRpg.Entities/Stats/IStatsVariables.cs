using OpenRpg.Core.Variables;

namespace OpenRpg.Entities.Stats
{
    /// <summary>
    /// Stat variables generally contain static stat data which is computed from effects active on the entity
    /// </summary>
    /// <remarks>
    /// Examples of stats would be MaxHealth, Strength, Damage, MovementSpeed, MaxAmmo, CanSeeInvisiblePeople.
    ///
    /// These values would likely not be serialized when state is being saved as these values can all be regenerated
    /// from the active effects/variables within the context of the entity, so for all intents and purposes see these
    /// as read only variables outside of the populators logic.
    /// </remarks>
    public interface IStatsVariables : IVariables<float>
    {}
}