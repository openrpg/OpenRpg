using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Data.InMemory.Builder
{
    public class InMemoryDataSourceBuilder
    {
        private readonly Dictionary<Type, Dictionary<object, object>> Database;

        protected InMemoryDataSourceBuilder(Dictionary<Type, Dictionary<object, object>> database)
        {
            Database = database;
        }

        public static InMemoryDataSourceBuilder Create()
        { return new InMemoryDataSourceBuilder(new Dictionary<Type, Dictionary<object, object>>()); }

        public InMemoryDataSourceBuilder WithData<T>(IEnumerable<T> data, Func<T, object> keySelector)
        {
            var typeOfT = typeof(T);
            if (!Database.ContainsKey(typeOfT))
            {
                var contents = data.ToDictionary(keySelector, x => (object)x);
                Database.Add(typeOfT, contents);
                return this;
            }

            var typeContainer = Database[typeOfT];
            
            foreach(var element in data)
            { typeContainer.Add(keySelector(element), element); }

            return this;
        }

        public IDataSource Build()
        { return new InMemoryDataSource(Database); }
    }
}