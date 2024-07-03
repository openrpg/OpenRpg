using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.TradeSkills.State;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class TradeSkillStateExtensions
    {
        public static int Logging(this TradeSkillState state) => state.Get(FantasyTradeSkillTypes.Logging);
        public static void Logging(this TradeSkillState state, int value) => state[FantasyTradeSkillTypes.Logging] = value;
        public static void AddLogging(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Logging, addition);
        
        public static int Mining(this TradeSkillState state) => state.Get(FantasyTradeSkillTypes.Mining);
        public static void Mining(this TradeSkillState state, int value) => state[FantasyTradeSkillTypes.Mining] = value;
        public static void AddMining(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Mining, addition);
        
        public static int Smelting(this TradeSkillState state) => state.Get(FantasyTradeSkillTypes.Smelting);
        public static void Smelting(this TradeSkillState state, int value) => state[FantasyTradeSkillTypes.Smelting] = value;
        public static void AddSmelting(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Smelting, addition);
        
        public static int Smithing(this TradeSkillState state) => state.Get(FantasyTradeSkillTypes.Smithing);
        public static void Smithing(this TradeSkillState state, int value) => state[FantasyTradeSkillTypes.Smithing] = value;
        public static void AddSmithing(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Smithing, addition);
    }
}