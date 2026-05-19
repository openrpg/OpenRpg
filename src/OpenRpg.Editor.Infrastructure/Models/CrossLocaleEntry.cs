using System.Collections.Generic;

namespace OpenRpg.Editor.Infrastructure.Models
{
    public class CrossLocaleEntry
    {
        public string LocaleId { get; set; }
        public List<LocaleEntry> Entries { get; set; } = new();
    }
}
