using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Races.Templates;
using OpenRpg.Data;
using OpenRpg.Data.InMemory;
using OpenRpg.Demos.Infrastructure.Builders;
using OpenRpg.Demos.Infrastructure.Data;
using OpenRpg.Demos.Infrastructure.DI;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Repositories;

namespace OpenRpg.Demos.Web.Modules
{
    public class OpenRpgDataModule : IModule
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<IDataSource>(x => GenerateDataSource());
            services.AddSingleton<ILocaleDataSource>(x => GenerateLocaleDataSource());
            services.AddSingleton<IRepository, DefaultRepository>();
            services.AddSingleton<ILocaleRepository>(x => new LocaleRepository(x.GetService<ILocaleDataSource>(), "en-gb"));
            services.AddSingleton<DemoCharacterBuilder>();
        }

        public InMemoryDataSource GenerateDataSource()
        {
            var itemTemplateGenerator = new ItemTemplateDataGenerator();
            var itemTemplateList = itemTemplateGenerator.GenerateData().ToList();
            var data = new Dictionary<Type, Dictionary<object, object>>();
            data.Add(typeof(RaceTemplate), new RaceTemplateDataGenerator().GenerateDictionary());
            data.Add(typeof(ClassTemplate), new ClassTemplateDataGenerator().GenerateDictionary());
            data.Add(typeof(ItemTemplate), itemTemplateGenerator.GenerateDictionary());
            data.Add(typeof(ItemGatheringTemplate), new GatheringTemplateDataGenerator().GenerateDictionary());
            data.Add(typeof(ItemCraftingTemplate), new CraftingTemplateDataGenerator().GenerateDictionary());
            return new InMemoryDataSource(data);
        }

        public InMemoryLocaleDataSource GenerateLocaleDataSource()
        {
            var data = new LocaleDataGenerator().GenerateData();
            return new InMemoryLocaleDataSource(data);
        }
    }
}