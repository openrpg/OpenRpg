using OpenRpg.Combat.Attacks;

namespace OpenRpg.Combat.Processors.Modifiers;

/// <summary>
/// One snapshot of an attack as it flows through the pipeline, with metadata
/// about which modifier produced it and whether it was applied.
/// </summary>
/// <param name="Snapshot">The attack state at this step.</param>
/// <param name="Modifier">The modifier that produced this step, or null for the base attack.</param>
/// <param name="WasApplied">True if the modifier ran and produced this snapshot, false if it was skipped.</param>
public record AttackModifierStep(Attack Snapshot, IAttackModifier? Modifier, bool WasApplied);