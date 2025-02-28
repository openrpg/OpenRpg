using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;

namespace OpenRpg.Demos.Infrastructure.Templates;

public class ManualTemplateAccessor : ITemplateAccessor
{
    public Dictionary<Type, Dictionary<int, ITemplate>> TemplateData { get; } = new Dictionary<Type, Dictionary<int, ITemplate>>();

    public void AddTemplate(ITemplate template)
    {
        var templateType = template.GetType();
        if(!TemplateData.ContainsKey(templateType))
        { TemplateData.Add(templateType, new Dictionary<int, ITemplate>()); }
        
        TemplateData[templateType].Add(template.Id, template);
    }
    
    public void AddTemplate<T>(T template) where T : ITemplate
    {
        var templateType = typeof(T);
        if(!TemplateData.ContainsKey(templateType))
        { TemplateData.Add(templateType, new Dictionary<int, ITemplate>()); }
        
        TemplateData[templateType].Add(template.Id, template);
    }
    
    public T Get<T>(int templateId) where T : ITemplate
    { return (T)TemplateData[typeof(T)][templateId]; }

    public IEnumerable<T> GetAll<T>() where T : ITemplate
    { return TemplateData[typeof(T)].Values.Cast<T>(); }
}