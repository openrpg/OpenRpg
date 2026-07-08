using OpenRpg.Core.Templates;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class ShipTemplate : ITemplate<ShipTemplateVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public ShipTemplateVariables Variables { get; set; } = new ShipTemplateVariables();
    }
}