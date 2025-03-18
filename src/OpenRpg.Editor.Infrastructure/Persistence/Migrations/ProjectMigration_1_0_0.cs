using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Editor.Infrastructure.Persistence.Migrations;

public class ProjectMigration_1_0_0 : IProjectMigration
{
    public string TargetVersion => "1.0.0";
    public string NewVersion => "1.0.1";
    
    public JObject MigrateLocale(JObject data) => data;

    public JArray MigrateTemplate<T>(JArray data)
    {
        foreach (var element in data)
        { MigrateTemplateElement<T>(element as JObject); }
        return data;
    }

    public void MigrateTemplateElement<T>(JObject element)
    {
        if(!element.ContainsKey("Effects")) { return; }

        var effectTypeContent = "OpenRpg.Entities.Effects.StaticEffect, OpenRpg.Entities";
        var effectArray = element["Effects"] as JArray;
        foreach (var effectNode in effectArray)
        {
            var effectObject = effectNode as JObject;
            effectObject.AddFirst(new JProperty("$type", effectTypeContent));
        }
    }
}