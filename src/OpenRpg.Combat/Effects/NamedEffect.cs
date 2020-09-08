using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace OpenRpg.Combat.Effects
{
    /// <summary>
    /// This is for pre-made named effects which have an id and locale details
    /// </summary>
    /// <remarks>This would be for effects like Poison 1 or Strength Buff IV etc</remarks>
    public class NamedEffect : Effect, IHasDataId, IHasLocaleDescription
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
    }
}