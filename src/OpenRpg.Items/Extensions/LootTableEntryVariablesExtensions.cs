using System.Collections.Generic;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class LootTableEntryVariablesExtensions
    {
        public static bool HasRequirements(this LootTableEntryVariables variables) 
            => variables.ContainsKey(LootTableEntryVariableTypes.Requirements);
        
        extension(LootTableEntryVariables vars)
        {
            /// <summary>
            /// The DropRate should be a value between 0-1 in which 0 is no drop chance, 0.5 is 50% drop chance, 1 is 100% drop chance
            /// </summary>
            /// <param name="value">The drop chance between 0-1</param>
            /// <returns>The drop chance can be as low as needed i.e a 1 in 1000 can be represented as 0.001</returns>
            public float DropRate
            {
                get => vars.GetFloat(LootTableEntryVariableTypes.DropRate);
                set => vars[LootTableEntryVariableTypes.DropRate] = value;
            }

            public bool IsUnique
            {
                get => vars.GetBool(LootTableEntryVariableTypes.IsUnique);
                set => vars[LootTableEntryVariableTypes.IsUnique] = value;
            }

            public IReadOnlyCollection<Requirement> Requirements
            {
                get => vars.GetAsOrDefaultAndSet(LootTableEntryVariableTypes.Requirements, () => new List<Requirement>());
                set => vars[LootTableEntryVariableTypes.Requirements] = value;
            }
        }
    }
}