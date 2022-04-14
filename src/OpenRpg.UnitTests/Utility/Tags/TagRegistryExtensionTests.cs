using System.Collections.Generic;
using System.Linq;
using OpenRpg.Tags;
using OpenRpg.Tags.Data;
using OpenRpg.Tags.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Utility.Tags;

public class TagRegistryExtensionTests
{
    [Fact]
    public void should_get_related_tags_with_averaged_scoring()
    {
        var armourTag = 1;
        var expensiveTag = 2;
        var heavyTag = 3;
        var metalTag = 4;

        // Should average 1.0 and 0 making 0.5, then return 0.4 for metal
        var existingData = new Dictionary<int, ICollection<TagWeighting>>
        {
            { armourTag, new List<TagWeighting> { new(expensiveTag, 1.0f), new (metalTag, 0.4f)} },
            { heavyTag, new List<TagWeighting> { new(expensiveTag, 0.0f)} }
        };
        var tagRegistry = new TagRegistry(existingData);
        
        var relatedTags = tagRegistry
            .GetRelatedTags(armourTag, heavyTag)
            .OrderBy(x => x.Tag)
            .ToArray();

        Assert.Equal(2, relatedTags.Length);
        Assert.Equal(expensiveTag, relatedTags[0].Tag);
        Assert.Equal(0.5f, relatedTags[0].Weight, 2);
        Assert.Equal(metalTag, relatedTags[1].Tag);
        Assert.Equal(0.4f, relatedTags[1].Weight, 2);
    }
    
    [Fact]
    public void should_get_related_tags_with_averaged_scoring_factoring_in_depth()
    {
        var armourTag = 1;
        var expensiveTag = 2;
        var heavyTag = 3;
        var lightTag = 4;
        var metalTag = 5;
        var leatherTag = 6;
        var woodTag = 7;
        
        var existingData = new Dictionary<int, ICollection<TagWeighting>>
        {
            { armourTag, new List<TagWeighting>
            {
                new(heavyTag, 0.5f), new(lightTag, 0.5f), 
                new(metalTag, 0.4f), new(woodTag, 0.2f)
            } },
            { heavyTag, new List<TagWeighting>
            {
                new(expensiveTag, 0.2f), new(metalTag, 0.8f), new(woodTag, 0.5f),
                new(leatherTag, -0.5f), new(lightTag, -0.5f)
            } },
            { metalTag, new List<TagWeighting>
            {
                new(expensiveTag, 0.4f), new(heavyTag, 0.8f)
            } },
            { woodTag, new List<TagWeighting>
            {
                new(expensiveTag, 0.2f), new(heavyTag, 0.3f)
            } }
        };
        
        var tagRegistry = new TagRegistry(existingData);
        var relatedTagsAtDepth2 = tagRegistry
            .GetRelatedTags(new[] { armourTag, heavyTag }, 2)
            .OrderBy(x => x.Tag)
            .ToArray();

        Assert.Equal(6, relatedTagsAtDepth2.Length);
        Assert.Equal(expensiveTag, relatedTagsAtDepth2[0].Tag);
        Assert.Equal(0.27f, relatedTagsAtDepth2[0].Weight, 2);
        Assert.Equal(heavyTag, relatedTagsAtDepth2[1].Tag);
        Assert.Equal(0.54f, relatedTagsAtDepth2[1].Weight, 2);
        Assert.Equal(lightTag, relatedTagsAtDepth2[2].Tag);
        Assert.Equal(-0.17f, relatedTagsAtDepth2[2].Weight, 2);
        Assert.Equal(metalTag, relatedTagsAtDepth2[3].Tag);
        Assert.Equal(0.67f, relatedTagsAtDepth2[3].Weight, 2);
        Assert.Equal(leatherTag, relatedTagsAtDepth2[4].Tag);
        Assert.Equal(-0.5f, relatedTagsAtDepth2[4].Weight, 2);
        Assert.Equal(woodTag, relatedTagsAtDepth2[5].Tag);
        Assert.Equal(0.4f, relatedTagsAtDepth2[5].Weight, 2);
    }
    
    [Fact]
    public void should_get_tagged_items_with_associated_score()
    {
        var armourTag = 1;
        var expensiveTag = 2;
        var heavyTag = 3;
        var lightTag = 4;
        var metalTag = 5;
        var leatherTag = 6;
        var woodTag = 7;
        
        var heavyMetalArmour = new TaggedEntity(armourTag, heavyTag, metalTag);
        var heavyWoodArmour = new TaggedEntity(armourTag, heavyTag, woodTag);
        var lightArmour = new TaggedEntity(armourTag, lightTag, leatherTag);
        var leatherBag = new TaggedEntity(expensiveTag, leatherTag);
        var taggedItems = new[] { heavyMetalArmour, heavyWoodArmour, lightArmour, leatherBag };

        var existingData = new Dictionary<int, ICollection<TagWeighting>>
        {
            { armourTag, new List<TagWeighting>
            {
                new(heavyTag, 0.5f), new(lightTag, 0.5f), 
                new(metalTag, 0.4f), new(woodTag, 0.2f)
            } },
            { heavyTag, new List<TagWeighting>
            {
                new(expensiveTag, 0.2f), new(metalTag, 0.8f), new(woodTag, 0.5f),
                new(leatherTag, -0.5f), new(lightTag, -0.5f)
            } },
        };
        
        var tagRegistry = new TagRegistry(existingData);
        var taggedItemsWithScore = tagRegistry
            .GetTaggedScoresFor(taggedItems, armourTag, heavyTag)
            .OrderByDescending(x => x.Score)
            .ToArray();

        Assert.Equal(2, taggedItemsWithScore.Length);
        Assert.Equal(heavyMetalArmour, taggedItemsWithScore[0].Entity);
        Assert.Equal(1.1f, taggedItemsWithScore[0].Score, 2);
        Assert.Equal(heavyWoodArmour, taggedItemsWithScore[1].Entity);
        Assert.Equal(0.85f, taggedItemsWithScore[1].Score, 2);
    }
    
}