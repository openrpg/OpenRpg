using System.Collections.Generic;
using System.Linq;
using OpenRpg.Tags;
using OpenRpg.Tags.Data;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Tags;

public class TagRegistryTests
{
    [Fact]
    public void should_correctly_add_relationships()
    {
        var expectedWeight = -0.8f;
        var illegalTag = 1;
        var lawfulTag = 2;

        var tagRegistry = new TagRegistry();
        tagRegistry.AddRelationship(illegalTag, lawfulTag, expectedWeight);

        Assert.Equal(1, tagRegistry.TagRelations.Count);
        Assert.True(tagRegistry.TagRelations.ContainsKey(illegalTag));
        Assert.Equal(1, tagRegistry.TagRelations[illegalTag].Count);
        Assert.Equal(new TagWeighting(lawfulTag, expectedWeight), tagRegistry.TagRelations[illegalTag].First());
    }
    
    [Fact]
    public void should_correctly_remove_relationships()
    {
        var illegalTag = 1;
        var lawfulTag = 2;

        var existingData = new Dictionary<int, ICollection<TagWeighting>>
        {
            { illegalTag, new List<TagWeighting> { new(lawfulTag, default)}}
        };
        var tagRegistry = new TagRegistry(existingData);
        tagRegistry.RemoveTagRelationship(illegalTag, lawfulTag);

        Assert.Equal(1, tagRegistry.TagRelations.Count);
        Assert.True(tagRegistry.TagRelations.ContainsKey(illegalTag));
        Assert.Equal(0, tagRegistry.TagRelations[illegalTag].Count);
    }
    
    [Fact]
    public void should_return_applicable_tags()
    {
        var illegalTag = 1;
        var lawfulTag = 2;
        var riskyTag = 3;

        var expectedWeightedTag1 = new TagWeighting(lawfulTag, -0.8f);
        var expectedWeightedTag2 = new TagWeighting(riskyTag, 0.5f);

        var existingData = new Dictionary<int, ICollection<TagWeighting>>
        {
            { illegalTag, new List<TagWeighting> { expectedWeightedTag1, expectedWeightedTag2 }}
        };
        var tagRegistry = new TagRegistry(existingData);
        var relatedTags = tagRegistry.GetRelatedTags(illegalTag).ToArray();

        Assert.Equal(2, relatedTags.Length);
        Assert.Contains(expectedWeightedTag1, relatedTags);
        Assert.Contains(expectedWeightedTag2, relatedTags);
    }
}