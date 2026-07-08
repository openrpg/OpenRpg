namespace OpenRpg.Demos.Battler.Code.Types;

/// <summary>
/// Shared constants for the Battler demo. Replaces magic numbers scattered across multiple files.
/// </summary>
public static class BattlerConstants
{
    // Party configuration
    public const int MaxPartySize = 4;
    public const int DefaultCharacterLevel = 1;

    // Battle configuration
    public const int DefaultEnemyCount = 6;
    public const double TurnDwellSeconds = 0.8;
    public const double TargetFlashSeconds = 0.3;

    // Starter item template IDs
    public const int PotionTemplateId = 20;
    public const int EtherTemplateId = 22;
    public const int PhoenixDownTemplateId = 23;
    public const int StarterPotionCount = 3;
    public const int StarterEtherCount = 1;
    public const int StarterPhoenixDownCount = 1;

    // Item type IDs (from Fantasy plugin)
    public static class ItemTypes
    {
        public const int Weapon = 2;
        public const int Head = 30;
        public const int Body = 31;
        public const int Legs = 32;
        public const int Back = 33;
        public const int Feet = 34;
        public const int Wrist = 35;
        public const int Neck = 36;
        public const int Ring = 37;
        public const int OffHand = 50;
        public const int Consumable = 60;
    }

    // Effect type IDs (from Fantasy/Genre plugins)
    public static class EffectTypes
    {
        public const int DamageBonus = 1;
        public const int DefenseBonus = 21;
        public const int MovementSpeedBonus = 44;
        public const int HealthBonus = 60;
        public const int HealthRestoreAmount = 62;
        public const int HealthRestorePercentage = 63;
        public const int ManaRestoreAmount = 236;
        public const int LifeRestoreAmount = 64;
        public const int LifeRestorePercentage = 65;
        public const int UnarmedDamageBonus = 263;
    }
}
