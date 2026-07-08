using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Demos.Infrastructure.Data;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// Sandbox-backed inventory-transaction playground. Holds a private Inventory plus
    /// a list of item templates the user can stage transactions against. Each Apply
    /// runs through the library's IInventoryTransaction and logs every step into an
    /// audit trail.
    /// </summary>
    public class InventoryTransactionDemoService : IInventoryTransactionDemoService
    {
        /// <summary>
        /// Slot cap on the sandbox inventory. Set high enough that the "Successful Trade"
        /// scenario (consume 3 ore + add 1 ingot) fits comfortably, but tight enough that
        /// the "Failed Addition" scenario (try to add 4 stackable-1 items when there's
        /// only one slot free) actually trips the slot check.
        /// </summary>
        public const int SandboxMaxSlots = 4;

        private readonly List<ItemData> _stagedAdditions = new();
        private readonly List<ItemData> _stagedRemovals = new();
        private readonly List<string> _auditLog = new();
        private readonly List<ItemData> _initialItems;

        public Inventory Sandbox { get; private set; } = new();
        public IReadOnlyList<ItemTemplate> AvailableTemplates { get; }
        public IReadOnlyList<ItemData> StagedAdditions => _stagedAdditions;
        public IReadOnlyList<ItemData> StagedRemovals => _stagedRemovals;
        public IReadOnlyList<string> AuditLog => _auditLog;

        private readonly ITemplateAccessor _templateAccessor;

        public InventoryTransactionDemoService(ITemplateAccessor templateAccessor)
        {
            _templateAccessor = templateAccessor;
            AvailableTemplates = new ItemTemplateDataGenerator().GenerateData().ToList();
            _initialItems = SeedInventory();
            Reset();
        }

        public void Reset()
        {
            Sandbox = new Inventory { Items = new List<ItemData>() };
            Sandbox.Variables[OpenRpg.Items.Types.InventoryVariableTypes.MaxSlots] = SandboxMaxSlots;
            foreach (var initial in _initialItems)
            {
                Sandbox.Items.Add(initial.Clone());
            }
            ClearStaging();
            _auditLog.Clear();
            Log("RESET: sandbox restored from seed (MaxSlots=" + SandboxMaxSlots + ")");
        }

        public void QueueRemoval(int itemTemplateId, int amount)
        {
            var data = new ItemData { TemplateId = itemTemplateId };
            if (amount > 0) { data.Variables.Amount = amount; }
            _stagedRemovals.Add(data);
            Log($"  + staged removal: template {itemTemplateId}" + (amount > 0 ? $" x{amount}" : ""));
        }

        public void QueueAddition(int itemTemplateId, int amount)
        {
            var data = new ItemData { TemplateId = itemTemplateId };
            if (amount > 0) { data.Variables.Amount = amount; }
            _stagedAdditions.Add(data);
            Log($"  + staged addition: template {itemTemplateId}" + (amount > 0 ? $" x{amount}" : ""));
        }

        public void ClearStaging()
        {
            if (_stagedRemovals.Count > 0 || _stagedAdditions.Count > 0)
            { Log("  + staging cleared (no changes applied)"); }
            _stagedRemovals.Clear();
            _stagedAdditions.Clear();
        }

        public bool ApplyStaged()
        {
            if (_stagedRemovals.Count == 0 && _stagedAdditions.Count == 0)
            {
                Log("  ! nothing to apply (staging is empty)");
                return false;
            }

            Log($"APPLY: {_stagedRemovals.Count} removals, {_stagedAdditions.Count} additions");
            var transaction = Sandbox.CreateTransaction(_templateAccessor);
            transaction.RemoveItems(_stagedRemovals.ToArray());
            transaction.AddItems(_stagedAdditions.ToArray());

            var succeeded = transaction.ApplyChanges();
            Log(succeeded ? "  => committed" : "  => rolled back (no changes applied)");
            _stagedRemovals.Clear();
            _stagedAdditions.Clear();
            return succeeded;
        }

        private void Log(string message)
        {
            var stamp = DateTime.Now.ToString("HH:mm:ss");
            _auditLog.Insert(0, $"[{stamp}] {message}");
            if (_auditLog.Count > 50)
            { _auditLog.RemoveRange(50, _auditLog.Count - 50); }
        }

        private static List<ItemData> SeedInventory()
        {
            return new List<ItemData>
            {
                AmountStack(ItemTemplateLookups.Sword, 1),
                AmountStack(ItemTemplateLookups.HealingPotion, 3),
                AmountStack(ItemTemplateLookups.CopperOre, 8)
            };
        }

        private static ItemData AmountStack(int templateId, int amount)
        {
            var data = new ItemData { TemplateId = templateId };
            data.Variables.Amount = amount;
            return data;
        }
    }
}
