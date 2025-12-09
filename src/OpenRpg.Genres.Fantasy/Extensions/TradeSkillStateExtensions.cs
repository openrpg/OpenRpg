using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.TradeSkills.State;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class TradeSkillStateExtensions
    {
        extension(TradeSkillState state)
        {
            public int Logging
            {
                get => state.Get(FantasyGatheringTradeSkillTypes.Logging);
                set => state[FantasyGatheringTradeSkillTypes.Logging] = value;
            }
            
            public int Mining
            {
                get => state.Get(FantasyGatheringTradeSkillTypes.Mining);
                set => state[FantasyGatheringTradeSkillTypes.Mining] = value;
            }
            
            public int Smelting
            {
                get => state.Get(FantasyCraftingTradeSkillTypes.Smelting);
                set => state[FantasyCraftingTradeSkillTypes.Smelting] = value;
            }
            
            public int Smithing
            {
                get => state.Get(FantasyCraftingTradeSkillTypes.Smithing);
                set => state[FantasyCraftingTradeSkillTypes.Smithing] = value;
            }
        }

        public static void AddLogging(this TradeSkillState state, int addition) => state.AddValue(FantasyGatheringTradeSkillTypes.Logging, addition);
        public static void AddMining(this TradeSkillState state, int addition) => state.AddValue(FantasyGatheringTradeSkillTypes.Mining, addition);
        public static void AddSmelting(this TradeSkillState state, int addition) => state.AddValue(FantasyCraftingTradeSkillTypes.Smelting, addition);
        public static void AddSmithing(this TradeSkillState state, int addition) => state.AddValue(FantasyCraftingTradeSkillTypes.Smithing, addition);
    }
}