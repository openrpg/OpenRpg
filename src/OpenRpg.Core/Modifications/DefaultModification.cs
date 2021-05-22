using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Modifications
{
    public class DefaultModification : IModification
    {
        public int Id { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = new Effect[0];
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public int ModificationType { get; set; }
    }
}