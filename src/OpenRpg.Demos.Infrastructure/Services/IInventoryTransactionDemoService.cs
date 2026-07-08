using System.Collections.Generic;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// Sandbox-backed inventory-transaction playground. Owns a private Inventory so CRUD
    /// operations here cannot leak into other demo pages. Every transaction call is logged
    /// into an audit trail for inspection.
    /// </summary>
    public interface IInventoryTransactionDemoService
    {
        /// <summary>The sandbox inventory. Read-only access for rendering.</summary>
        Inventory Sandbox { get; }

        /// <summary>Item templates the demo is allowed to operate on.</summary>
        IReadOnlyList<ItemTemplate> AvailableTemplates { get; }

        /// <summary>Items currently queued as additions on the staged transaction.</summary>
        IReadOnlyList<ItemData> StagedAdditions { get; }

        /// <summary>Items currently queued as removals on the staged transaction.</summary>
        IReadOnlyList<ItemData> StagedRemovals { get; }

        /// <summary>Timestamped log of every transaction step and outcome.</summary>
        IReadOnlyList<string> AuditLog { get; }

        void Reset();

        /// <summary>Queues a removal. <paramref name="amount"/> is 0 for non-amounted items.</summary>
        void QueueRemoval(int itemTemplateId, int amount);

        /// <summary>Queues an addition. <paramref name="amount"/> is 0 for non-amounted items.</summary>
        void QueueAddition(int itemTemplateId, int amount);

        /// <summary>Clears the staged additions/removals without applying.</summary>
        void ClearStaging();

        /// <summary>
        /// Applies the staged transaction. Logs every step (queued operations, attempts, rollback,
        /// final result) into <see cref="AuditLog"/>.
        /// </summary>
        /// <returns>True if all changes committed; false if anything rolled back.</returns>
        bool ApplyStaged();
    }
}
