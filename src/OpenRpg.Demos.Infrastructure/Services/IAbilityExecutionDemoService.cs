using System.Collections.Generic;
using OpenRpg.Combat.Abilities;

namespace OpenRpg.Demos.Infrastructure.Services
{
    /// <summary>
    /// Sandbox-backed ability-execution playground. Owns a private party of casters and a
    /// private enemy formation so casts here cannot leak into other demo pages. Every cast
    /// runs through the library's attack generators + processors (mirroring the Battler
    /// demo's <c>AbilityExecutor</c> + <c>TargetResolver</c>) and logs each step into an
    /// audit trail.
    /// </summary>
    public interface IAbilityExecutionDemoService
    {
        /// <summary>Friendly casters in the sandbox, in display order. Recomputed on each read so HP/MP reflect current state.</summary>
        IReadOnlyList<CasterSnapshot> Party { get; }

        /// <summary>Enemy formation in the sandbox, in display order. Recomputed on each read so HP reflects current state.</summary>
        IReadOnlyList<TargetSnapshot> Enemies { get; }

        /// <summary>All ability templates the demo knows about.</summary>
        IReadOnlyList<AbilityOption> AvailableAbilities { get; }

        /// <summary>
        /// Returns the subset of <see cref="AvailableAbilities"/> the given caster can use
        /// (class requirement met, alive, has enough mana for at least the base cost).
        /// </summary>
        IReadOnlyList<AbilityOption> GetAbilitiesForCaster(int casterIndex);

        /// <summary>True if the given caster can currently use the given ability (alive, class ok, has mana).</summary>
        bool CanCasterUseAbility(int casterIndex, int abilityTemplateId);

        /// <summary>Templated state used to seed the sandbox. Captured so <see cref="Reset"/> can restore it.</summary>
        SandboxSnapshot InitialState { get; }

        /// <summary>Templated log of every cast step and outcome. Most recent first.</summary>
        IReadOnlyList<string> CombatLog { get; }

        /// <summary>The list of target indexes that were hit on the most recent cast. Used by the page to highlight rows.</summary>
        IReadOnlyList<int> LastCastEnemyTargetIndexes { get; }

        /// <summary>The list of caster indexes that were healed on the most recent cast (heals target allies).</summary>
        IReadOnlyList<int> LastCastAllyTargetIndexes { get; }

        /// <summary>The id of the ability cast on the most recent cast, or 0 if none yet.</summary>
        int LastCastAbilityId { get; }

        void Reset();

        /// <summary>
        /// Casts an ability. Logs the resolve-target, mana-deduct, attack-generate, and damage-apply
        /// steps into <see cref="CombatLog"/>. Returns <c>true</c> if the cast committed (regardless
        /// of whether the targets were in range and took damage).
        /// </summary>
        bool CastAbility(int casterIndex, int abilityTemplateId);
    }

    /// <summary>Read-only snapshot of a friendly caster for rendering.</summary>
    public record CasterSnapshot(
        int Index,
        string Name,
        int ClassId,
        string ClassName,
        int MaxHp,
        int CurrentHp,
        int MaxMana,
        int CurrentMana,
        bool IsAlive);

    /// <summary>Read-only snapshot of an enemy target for rendering.</summary>
    public record TargetSnapshot(
        int Index,
        string Name,
        int MaxHp,
        int CurrentHp,
        bool IsAlive);

    /// <summary>Wraps an <see cref="AbilityTemplate"/> with the resolved name for the dropdown.</summary>
    public record AbilityOption(int Id, string Name, string Description, int TargetType, int TargetCount, int ManaCost, AbilityTemplate Template);

    /// <summary>Initial sandbox state used to restore the demo on <see cref="IAbilityExecutionDemoService.Reset"/>.</summary>
    public record SandboxSnapshot(IReadOnlyList<CasterSnapshot> Party, IReadOnlyList<TargetSnapshot> Enemies);
}
