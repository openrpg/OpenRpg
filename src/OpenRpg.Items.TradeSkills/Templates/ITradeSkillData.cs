namespace OpenRpg.Items.TradeSkills.Templates
{
    /// <summary>
    /// The core mechanical information common among most trade skills
    /// </summary>
    public interface ITradeSkillData
    {
        /// <summary>
        /// Gathering time in seconds, per unit gathered
        /// </summary>
        public float TimeToComplete { get; set; }

        /// <summary>
        /// The category of skill type used for Gathering
        /// </summary>
        public int SkillType { get; set; }
    
        /// <summary>
        /// Indicates how difficult this is to get, effects if you can use the trade skill and skill up rates
        /// </summary>
        public int SkillDifficulty { get; set; }
    }
}