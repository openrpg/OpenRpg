using System.Collections.Generic;
using System.Linq;
using OpenRpg.Tags;
using OpenRpg.Tags.Data;
using OpenRpg.Tags.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Tags;

public class ITaggedExtensionTests
{
    [Fact]
    public void should_correctly_verify_entity_contains_tags()
    {
        var tag1 = 1;
        var tag2 = 2;
        var tag3 = 3;
        var tag4 = 4;
        var entity123 = new TagList(tag1, tag2, tag3);
        var entity34 = new TagList(tag3, tag4);

        Assert.True(entity123.ContainsTags(tag1, tag2));
        Assert.False(entity34.ContainsTags(tag1, tag2));
        Assert.False(entity123.ContainsTags(tag1, tag4));
    }
    
    [Fact]
    public void should_correctly_verify_entity_doesnt_contains_tags()
    {
        var tag1 = 1;
        var tag2 = 2;
        var tag3 = 3;
        var tag4 = 4;
        var entity123 = new TagList(tag1, tag2, tag3);
        var entity34 = new TagList(tag3, tag4);

        Assert.True(entity123.DoesNotContainsTags(tag4));
        Assert.False(entity123.DoesNotContainsTags(tag3, tag4));
        Assert.True(entity34.DoesNotContainsTags(tag1, tag2));
    }
    
    [Fact]
    public void should_correctly_return_entities_containing_tags()
    {
        var tag1 = 1;
        var tag2 = 2;
        var tag3 = 3;
        var tag4 = 4;
        var entity123 = new ContextualTags(123, new TagList(tag1, tag2, tag3));
        var entity23 = new ContextualTags(23, new TagList(tag2, tag3));
        var entity13 = new ContextualTags(13, new TagList(tag1, tag3));
        var entity34 = new ContextualTags(34, new TagList(tag3, tag4));
        var entity1234 = new ContextualTags(1234, new TagList(tag1, tag2, tag3, tag4));
        var allEntities = new[] { entity13, entity23, entity34, entity123, entity1234 };

        var entitiesMatching1and3 = allEntities.ContainingTags(tag1, tag3).ToArray();
        var entitiesMatching1and2and3 = allEntities.ContainingTags(tag1, tag2, tag3).ToArray();
        
        Assert.Equal(3, entitiesMatching1and3.Length);
        Assert.Contains(entity13, entitiesMatching1and3);
        Assert.Contains(entity123, entitiesMatching1and3);
        Assert.Contains(entity1234, entitiesMatching1and3);
        
        Assert.Equal(2, entitiesMatching1and2and3.Length);
        Assert.Contains(entity123, entitiesMatching1and2and3);
        Assert.Contains(entity1234, entitiesMatching1and2and3);
    }
}