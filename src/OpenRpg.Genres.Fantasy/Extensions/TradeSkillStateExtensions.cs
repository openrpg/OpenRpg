using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.TradeSkills.State;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class TradeSkillStateExtensions
    {
        public static int Logging(this TradeSkillState state) => state.Get(FantasyGatheringTradeSkillTypes.Logging);
        public static void Logging(this TradeSkillState state, int value) => state[FantasyGatheringTradeSkillTypes.Logging] = value;
        public static void AddLogging(this TradeSkillState state, int addition) => state.AddValue(FantasyGatheringTradeSkillTypes.Logging, addition);
        
        public static int Mining(this TradeSkillState state) => state.Get(FantasyGatheringTradeSkillTypes.Mining);
        public static void Mining(this TradeSkillState state, int value) => state[FantasyGatheringTradeSkillTypes.Mining] = value;
        public static void AddMining(this TradeSkillState state, int addition) => state.AddValue(FantasyGatheringTradeSkillTypes.Mining, addition);
        
        public static int Smelting(this TradeSkillState state) => state.Get(FantasyCraftingTradeSkillTypes.Smelting);
        public static void Smelting(this TradeSkillState state, int value) => state[FantasyCraftingTradeSkillTypes.Smelting] = value;
        public static void AddSmelting(this TradeSkillState state, int addition) => state.AddValue(FantasyCraftingTradeSkillTypes.Smelting, addition);
        
        public static int Smithing(this TradeSkillState state) => state.Get(FantasyCraftingTradeSkillTypes.Smithing);
        public static void Smithing(this TradeSkillState state, int value) => state[FantasyCraftingTradeSkillTypes.Smithing] = value;
        public static void AddSmithing(this TradeSkillState state, int addition) => state.AddValue(FantasyCraftingTradeSkillTypes.Smithing, addition);
    }
}