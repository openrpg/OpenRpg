using System.Collections.Generic;
using OpenRpg.Effects;
using OpenRpg.Items.Modifications;

namespace OpenRpg.Items.Defaults
{
    public class Modification : IModification
    {
        public int Id { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public int ModificationType { get; set; }
    }
}