using OpenRpg.Combat.Abilities.Variables;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Combat
{
    public class CombatAbilityTemplateVariableExtensionTests
    {
        [Fact]
        public void should_get_default_cooldown()
        {
            var vars = new AbilityTemplateVariables();
            Assert.Equal(0f, vars.Cooldown);
        }

        [Fact]
        public void should_apply_cooldown()
        {
            var vars = new AbilityTemplateVariables();
            vars.Cooldown = 5f;
            Assert.Equal(5f, vars.Cooldown);
        }

        [Fact]
        public void should_identify_has_cooldown()
        {
            var vars = new AbilityTemplateVariables();
            vars.Cooldown = 5f;
            Assert.True(vars.HasCooldown());
        }

        [Fact]
        public void should_identify_not_have_cooldown()
        {
            var vars = new AbilityTemplateVariables();
            Assert.False(vars.HasCooldown());
        }

        [Fact]
        public void should_get_default_range()
        {
            var vars = new AbilityTemplateVariables();
            Assert.Equal(0f, vars.Range);
        }

        [Fact]
        public void should_apply_range()
        {
            var vars = new AbilityTemplateVariables();
            vars.Range = 10f;
            Assert.Equal(10f, vars.Range);
        }

        [Fact]
        public void should_identify_has_range()
        {
            var vars = new AbilityTemplateVariables();
            vars.Range = 10f;
            Assert.True(vars.HasRange());
        }

        [Fact]
        public void should_apply_attack_size()
        {
            var vars = new AbilityTemplateVariables();
            vars.AttackSize = 3f;
            Assert.Equal(3f, vars.AttackSize);
        }

        [Fact]
        public void should_identify_has_attack_size()
        {
            var vars = new AbilityTemplateVariables();
            vars.AttackSize = 3f;
            Assert.True(vars.HasAttackSize());
        }

        [Fact]
        public void should_get_default_damage()
        {
            var vars = new AbilityTemplateVariables();
            var damage = vars.Damage;
            Assert.Equal(0, damage.Type);
            Assert.Equal(0f, damage.Value);
        }

        [Fact]
        public void should_apply_damage()
        {
            var vars = new AbilityTemplateVariables();
            vars.Damage = new Damage(1, 50f);
            Assert.Equal(1, vars.Damage.Type);
            Assert.Equal(50f, vars.Damage.Value);
        }

        [Fact]
        public void should_identify_has_damage()
        {
            var vars = new AbilityTemplateVariables();
            vars.Damage = new Damage(1, 50f);
            Assert.True(vars.HasDamage());
        }

        [Fact]
        public void should_identify_not_have_damage()
        {
            var vars = new AbilityTemplateVariables();
            Assert.False(vars.HasDamage());
        }

        [Fact]
        public void should_apply_target_type()
        {
            var vars = new AbilityTemplateVariables();
            vars.TargetType = 2;
            Assert.Equal(2, vars.TargetType);
        }

        [Fact]
        public void should_identify_has_target_type()
        {
            var vars = new AbilityTemplateVariables();
            vars.TargetType = 2;
            Assert.True(vars.HasTargetType());
        }

        [Fact]
        public void should_apply_multi_hit()
        {
            var vars = new AbilityTemplateVariables();
            vars.MultiHit = 3;
            Assert.Equal(3, vars.MultiHit);
        }

        [Fact]
        public void should_identify_has_multi_hit()
        {
            var vars = new AbilityTemplateVariables();
            vars.MultiHit = 3;
            Assert.True(vars.HasMultiHit());
        }

        [Fact]
        public void should_apply_target_count()
        {
            var vars = new AbilityTemplateVariables();
            vars.TargetCount = 5;
            Assert.Equal(5, vars.TargetCount);
        }

        [Fact]
        public void should_identify_has_target_count()
        {
            var vars = new AbilityTemplateVariables();
            vars.TargetCount = 5;
            Assert.True(vars.HasTargetCount());
        }
    }
}
