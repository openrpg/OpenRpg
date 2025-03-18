using Newtonsoft.Json.Linq;

namespace OpenRpg.Editor.Infrastructure.Persistence.Migrations;

public interface IProjectMigration
{
    string TargetVersion { get; }
    string NewVersion { get; }

    JArray MigrateTemplate<T>(JArray data);
    JObject MigrateLocale(JObject data);
}