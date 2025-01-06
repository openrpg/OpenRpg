using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Templates;

namespace OpenRpg.Core.Classes
{
    public class ClassData : ITemplateData<ClassVariables>
    {
        public int TemplateId { get; set; }
        public ClassVariables Variables { get; set; } = new ClassVariables();
    }
}