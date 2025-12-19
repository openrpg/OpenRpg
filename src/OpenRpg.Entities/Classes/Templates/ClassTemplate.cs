using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Classes.Templates
{
    public class ClassTemplate : ITemplate<ClassTemplateVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public ClassTemplateVariables Variables { get; set; } = new ClassTemplateVariables();
    }
}