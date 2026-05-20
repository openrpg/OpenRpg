using System;
using System.Collections.Generic;

namespace OpenRpg.Demos.Battler.Code.Types;

public static class ClassLookups
{
    public const int Warrior = 1;
    public const int Monk = 2;
    public const int WhiteMage = 3;
    public const int Thief = 4;
    public const int BlackMage = 5;

    public static readonly int[] AllClassIds = [Warrior, Monk, WhiteMage, Thief, BlackMage];

    private static readonly Random _rng = new();

    public static int[] GetRandomPartyIds(int count = 4)
    {
        var pool = new List<int>(AllClassIds);
        var result = new int[count];
        for (var i = 0; i < count; i++)
        {
            var idx = _rng.Next(pool.Count);
            result[i] = pool[idx];
            pool.RemoveAt(idx);
        }
        return result;
    }
}
