using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Factions;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class FactionReputationTests
{
    [Fact]
    public void add_reputation_should_start_at_zero_for_new_faction()
    {
        var rep = new FactionReputation();

        var result = rep.AddReputation(1, 10);

        Assert.Equal(10, result);
        Assert.Equal(10, rep[1]);
    }

    [Fact]
    public void add_reputation_should_accumulate()
    {
        var rep = new FactionReputation();
        rep.AddReputation(1, 10);

        var result = rep.AddReputation(1, 20);

        Assert.Equal(30, result);
        Assert.Equal(30, rep[1]);
    }

    [Fact]
    public void add_reputation_should_handle_negative_values()
    {
        var rep = new FactionReputation();
        rep[1] = 10;

        var result = rep.AddReputation(1, -5);

        Assert.Equal(5, result);
        Assert.Equal(5, rep[1]);
    }

    [Fact]
    public void add_reputation_should_return_new_value()
    {
        var rep = new FactionReputation();

        var result1 = rep.AddReputation(1, 25);
        var result2 = rep.AddReputation(1, 15);

        Assert.Equal(25, result1);
        Assert.Equal(40, result2);
    }
}
