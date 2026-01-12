namespace OpenRpg.Combat.Types
{
    public interface CombatAbilityTemplateVariableTypes
    {
        public static readonly int Unknown = 0;
        
        public static readonly int Cooldown = 1;
        public static readonly int Damage = 2;
        public static readonly int Range = 3;
        public static readonly int AttackSize = 4;
        public static readonly int TargetType = 5;
        public static readonly int MultiHit = 6;
        public static readonly int TargetCount = 7;
    }
}