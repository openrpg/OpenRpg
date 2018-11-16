using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;

namespace OpenRpg.Items.Defaults
{
    public class Modification : IModification
    {
        public int Id { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = new List<Effect>();
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public int ModificationType { get; set; }
    }
}