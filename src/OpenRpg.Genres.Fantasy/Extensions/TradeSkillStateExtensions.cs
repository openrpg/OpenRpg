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
                get => state.Get(FantasyTradeSkillTypes.Logging);
                set => state[FantasyTradeSkillTypes.Logging] = value;
            }
            
            public int Mining
            {
                get => state.Get(FantasyTradeSkillTypes.Mining);
                set => state[FantasyTradeSkillTypes.Mining] = value;
            }
            
            public int Smelting
            {
                get => state.Get(FantasyTradeSkillTypes.Smelting);
                set => state[FantasyTradeSkillTypes.Smelting] = value;
            }
            
            public int Smithing
            {
                get => state.Get(FantasyTradeSkillTypes.Smithing);
                set => state[FantasyTradeSkillTypes.Smithing] = value;
            }
        }

        public static void AddLogging(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Logging, addition);
        public static void AddMining(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Mining, addition);
        public static void AddSmelting(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Smelting, addition);
        public static void AddSmithing(this TradeSkillState state, int addition) => state.AddValue(FantasyTradeSkillTypes.Smithing, addition);
    }
}