using System;
using System.Collections.Generic;

namespace OpenRpg.Combat.Attacks
{
    public record Attack(bool IsCritical, IReadOnlyList<Damage> Damages);
}