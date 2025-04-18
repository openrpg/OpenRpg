namespace OpenRpg.Combat.Types
{
    public interface CombatTargetTypes
    {
        public static readonly int Unknown = 0;
        
        public static readonly int SingleTarget = 1;
        public static readonly int MultipleTarget = 2;
        
        public static readonly int SinglePosition = 3;
        public static readonly int MultiplePositions = 4;
        
        public static readonly int AreaOfEffect = 5;
        public static readonly int MultipleAreaOfEffects = 6;
    }
}