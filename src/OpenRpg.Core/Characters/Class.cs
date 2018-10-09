using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Localization;
using OpenRpg.Core.Requirements;
using OpenRpg.Effects;

namespace OpenRpg.Core.Characters
{
    public class Class : IHasDataId, IHasAssetCode, IHasEffects, IHasRequirements, IHasLocaleDescription
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}