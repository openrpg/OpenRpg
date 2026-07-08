using System;
using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Data;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// Provides a sandboxed CRUD playground that operates on a private copy of the demo
    /// template data, isolated from the shared IDataSource used by the rest of the app.
    /// </summary>
    public interface IPersistenceDemoService
    {
        /// <summary>
        /// Frozen snapshot of the seed data captured at construction. Used by <see cref="Reset"/>.
        /// </summary>
        IReadOnlyDictionary<Type, object> SeedSnapshot { get; }

        /// <summary>
        /// The internal repository wired to the sandboxed InMemoryDataSource. Exposed so
        /// callers can demonstrate the raw IRepository.Query(IQuery&lt;T&gt;) path in addition
        /// to the typed helpers.
        /// </summary>
        IRepository Repository { get; }

        /// <summary>
        /// Rebuilds the sandboxed store from the captured seed snapshot.
        /// </summary>
        void Reset();

        IReadOnlyCollection<T> GetAll<T>() where T : class, IHasDataId;
        T Get<T>(object id) where T : class, IHasDataId;
        bool Exists<T>(object id) where T : class, IHasDataId;

        /// <summary>
        /// Persists <paramref name="entity"/>. If its <see cref="IHasDataId.Id"/> is 0, a new id
        /// is allocated via <see cref="NextIdFor{T}"/>.
        /// </summary>
        T Create<T>(T entity) where T : class, IHasDataId;

        T Update<T>(T entity) where T : class, IHasDataId;
        bool Delete<T>(object id) where T : class, IHasDataId;

        /// <summary>
        /// Returns the next available id for <typeparamref name="T"/>, which is <c>Max(existing) + 1</c>
        /// (or <c>1</c> if the store is empty).
        /// </summary>
        int NextIdFor<T>() where T : class, IHasDataId;
    }
}
