using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Items.TradeSkills.State;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class TradeSkillStateExtensionTests
    {
        [Fact]
        public void should_get_default_logging()
        {
            var state = new TradeSkillState();
            Assert.Equal(0, state.Logging);
        }

        [Fact]
        public void should_apply_logging()
        {
            var state = new TradeSkillState();
            state.Logging = 10;
            Assert.Equal(10, state.Logging);
        }

        [Fact]
        public void should_add_logging()
        {
            var state = new TradeSkillState();
            state.Logging = 10;
            state.AddLogging(5);
            Assert.Equal(15, state.Logging);
        }

        [Fact]
        public void should_get_default_mining()
        {
            var state = new TradeSkillState();
            Assert.Equal(0, state.Mining);
        }

        [Fact]
        public void should_apply_mining()
        {
            var state = new TradeSkillState();
            state.Mining = 20;
            Assert.Equal(20, state.Mining);
        }

        [Fact]
        public void should_add_mining()
        {
            var state = new TradeSkillState();
            state.Mining = 10;
            state.AddMining(5);
            Assert.Equal(15, state.Mining);
        }

        [Fact]
        public void should_get_default_smelting()
        {
            var state = new TradeSkillState();
            Assert.Equal(0, state.Smelting);
        }

        [Fact]
        public void should_apply_smelting()
        {
            var state = new TradeSkillState();
            state.Smelting = 15;
            Assert.Equal(15, state.Smelting);
        }

        [Fact]
        public void should_add_smelting()
        {
            var state = new TradeSkillState();
            state.Smelting = 10;
            state.AddSmelting(5);
            Assert.Equal(15, state.Smelting);
        }

        [Fact]
        public void should_get_default_smithing()
        {
            var state = new TradeSkillState();
            Assert.Equal(0, state.Smithing);
        }

        [Fact]
        public void should_apply_smithing()
        {
            var state = new TradeSkillState();
            state.Smithing = 25;
            Assert.Equal(25, state.Smithing);
        }

        [Fact]
        public void should_add_smithing()
        {
            var state = new TradeSkillState();
            state.Smithing = 10;
            state.AddSmithing(5);
            Assert.Equal(15, state.Smithing);
        }
    }
}
