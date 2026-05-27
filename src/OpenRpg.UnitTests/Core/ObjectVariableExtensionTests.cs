using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Tags;
using Xunit;

namespace OpenRpg.UnitTests.Core
{
    public class ObjectVariableExtensionTests
    {
        [Fact]
        public void should_get_default_asset_code()
        {
            var vars = new ClassVariables();
            Assert.Equal(string.Empty, vars.AssetCode);
        }

        [Fact]
        public void should_apply_asset_code()
        {
            var vars = new ClassVariables();
            vars.AssetCode = "item_001";
            Assert.Equal("item_001", vars.AssetCode);
        }

        [Fact]
        public void should_identify_has_asset_code()
        {
            var vars = new ClassVariables();
            vars.AssetCode = "item_001";
            Assert.True(vars.HasAssetCode());
        }

        [Fact]
        public void should_identify_not_have_asset_code()
        {
            var vars = new ClassVariables();
            Assert.False(vars.HasAssetCode());
        }

        [Fact]
        public void should_get_default_tags()
        {
            var vars = new ClassVariables();
            var tags = vars.Tags;
            Assert.Empty(tags);
        }

        [Fact]
        public void should_apply_tags()
        {
            var vars = new ClassVariables();
            vars.Tags = new TagList { 1, 2, 3 };
            Assert.Equal(3, vars.Tags.Count);
            Assert.Contains(1, vars.Tags);
            Assert.Contains(2, vars.Tags);
            Assert.Contains(3, vars.Tags);
        }

        [Fact]
        public void should_identify_has_tags()
        {
            var vars = new ClassVariables();
            vars.Tags = new TagList { 1 };
            Assert.True(vars.HasTags());
        }

        [Fact]
        public void should_identify_not_have_tags()
        {
            var vars = new ClassVariables();
            Assert.False(vars.HasTags());
        }
    }
}
