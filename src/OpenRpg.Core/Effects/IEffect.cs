using System.Collections.Generic;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Effects
{
    /// <summary>
    /// Generic effect interface that lets us all agree every effect has a type and requirements
    /// </summary>
    public interface IEffect : IHasRequirements
    {
        /// <summary>
        /// The effect type to apply
        /// </summary>
        int EffectType { get; set; }
        
        /// <summary>
        /// Requirements required for this object to function/be used
        /// </summary>
        new IReadOnlyCollection<Requirement> Requirements { get; set; }
    }
}