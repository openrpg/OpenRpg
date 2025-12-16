using OpenRpg.Entities.Entity.Variables;

namespace OpenRpg.Entities.Entity.Templates;

public class EntityTemplate : IEntityTemplate
{
    public int Id { get; set; }
    public string NameLocaleId { get; set; } = string.Empty;
    public string DescriptionLocaleId { get; set; } = string.Empty;
    
    public EntityTemplateVariables Variables { get; set; } = new();
}