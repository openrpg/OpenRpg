using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Common;
using OpenRpg.Data;
using OpenRpg.Data.Conventions.Extensions;
using OpenRpg.Data.InMemory;
using OpenRpg.Demos.Infrastructure.Data;
using OpenRpg.Demos.Infrastructure.Extensions;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// Sandbox-backed persistence playground. Holds a private InMemoryDataSource and Repository
    /// so CRUD operations here cannot leak into the shared IRepository used by other demo pages.
    /// </summary>
    public class PersistenceDemoService : IPersistenceDemoService
    {
        private readonly Dictionary<Type, Dictionary<object, object>> _initialSeed;
        private InMemoryDataSource _sandboxDataSource;

        public IReadOnlyDictionary<Type, object> SeedSnapshot { get; }

        public IRepository Repository { get; private set; }

        public PersistenceDemoService()
        {
            _initialSeed = BuildInitialSeed();
            SeedSnapshot = FreezeSeedSnapshot(_initialSeed);
            RebuildSandbox();
        }

        public void Reset() => RebuildSandbox();

        public IReadOnlyCollection<T> GetAll<T>() where T : class, IHasDataId
        {
            return Repository.GetAll<T>().ToList();
        }

        public T Get<T>(object id) where T : class, IHasDataId
        {
            return Repository.Get<T>(id);
        }

        public bool Exists<T>(object id) where T : class, IHasDataId
        {
            return Repository.Exists<T>(id);
        }

        public T Create<T>(T entity) where T : class, IHasDataId
        {
            if (entity.Id == 0)
            {
                var newId = NextIdFor<T>();
                SetEntityId(entity, newId);
            }
            return Repository.Create(entity);
        }

        public T Update<T>(T entity) where T : class, IHasDataId
        {
            return Repository.Update(entity);
        }

        public bool Delete<T>(object id) where T : class, IHasDataId
        {
            return Repository.Delete<T>(id);
        }

        public int NextIdFor<T>() where T : class, IHasDataId
        {
            var existing = Repository.GetAll<T>().ToList();
            return existing.Count == 0 ? 1 : existing.Max(e => e.Id) + 1;
        }

        private void RebuildSandbox()
        {
            var copy = new Dictionary<Type, Dictionary<object, object>>();
            foreach (var kvp in _initialSeed)
            {
                var inner = new Dictionary<object, object>();
                foreach (var entry in kvp.Value)
                {
                    inner[entry.Key] = entry.Value;
                }
                copy[kvp.Key] = inner;
            }

            _sandboxDataSource = new InMemoryDataSource(copy);
            Repository = new Repository(_sandboxDataSource);
        }

        private static Dictionary<Type, Dictionary<object, object>> BuildInitialSeed()
        {
            var data = new Dictionary<Type, Dictionary<object, object>>();
            AddIfRegistered(data, typeof(OpenRpg.Entities.Races.Templates.RaceTemplate), new Data.RaceTemplateDataGenerator());
            AddIfRegistered(data, typeof(OpenRpg.Items.Templates.ItemTemplate), new Data.ItemTemplateDataGenerator());
            AddIfRegistered(data, typeof(OpenRpg.Quests.QuestTemplate), new Data.QuestStateDataGenerator());
            return data;
        }

        private static void AddIfRegistered<T>(Dictionary<Type, Dictionary<object, object>> data, Type type, IDataGenerator<T> generator)
            where T : IHasDataId
        {
            if (!data.ContainsKey(type))
            {
                data.Add(type, generator.GenerateDictionary());
            }
        }

        private static IReadOnlyDictionary<Type, object> FreezeSeedSnapshot(Dictionary<Type, Dictionary<object, object>> seed)
        {
            var snapshot = new Dictionary<Type, object>(seed.Count);
            foreach (var kvp in seed)
            {
                snapshot[kvp.Key] = new Dictionary<object, object>(kvp.Value);
            }
            return snapshot;
        }

        private static void SetEntityId<T>(T entity, int newId) where T : IHasDataId
        {
            var prop = typeof(T).GetProperty(nameof(IHasDataId.Id));
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, newId);
            }
        }
    }
}
