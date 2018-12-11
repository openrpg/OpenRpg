using System.Collections.Generic;
using OpenRpg.Localization.Repositories;

namespace OpenRpg.Localization.Databases
{
    public interface ILocaleDatabase
    {
        ILocaleRepository GetRepository(string localeCode);
        void AddRepository(ILocaleRepository repository);
        void RemoveRepository(string dataset);
        IEnumerable<ILocaleRepository> GetAllRepositories();
    }
}