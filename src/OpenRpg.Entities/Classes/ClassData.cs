using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes.Variables;

namespace OpenRpg.Entities.Classes
{
    public class ClassData : ITemplateData<ClassVariables>
    {
        public int TemplateId { get; set; }
        public ClassVariables Variables { get; set; } = new ClassVariables();
    }
}