using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Effects
{
    /// <summary>
    /// Generic effect interface that lets us all agree every effect has a type
    /// </summary>
    public interface IEffect : IHasRequirements
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        public int EffectType { get; }
    }
}