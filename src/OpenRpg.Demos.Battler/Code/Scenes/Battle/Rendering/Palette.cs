using Microsoft.Xna.Framework;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;

public static class Palette
{
    // HP bar colors
    public static readonly Color HpGreen = new(80, 200, 60);
    public static readonly Color HpYellow = new(220, 200, 40);
    public static readonly Color HpRed = new(200, 40, 40);
    public static readonly Color HpBarBg = new(30, 30, 30);

    // Entity colors
    public static readonly Color Background = new(20, 20, 30);
    public static readonly Color PlayerRect = new(30, 60, 140);
    public static readonly Color PlayerRectDead = new(20, 25, 40);
    public static readonly Color EnemyRect = new(140, 30, 30);
    public static readonly Color EnemyRectDead = new(40, 20, 20);

    // Battle panel colors
    public static readonly Color PanelBg = new(10, 10, 25);
    public static readonly Color PanelHpBarBg = new(40, 40, 50);
    public static readonly Color EnemyName = new(220, 150, 150);
    public static readonly Color PartyName = new(150, 180, 220);
    public static readonly Color HpText = new(200, 200, 200);
    public static readonly Color MpText = new(100, 160, 255);

    // Turn order strip colors
    public static readonly Color StripBg = new Color(10, 10, 25) * 0.85f;
    public static readonly Color CurrentChip = new(70, 70, 95);
    public static readonly Color FutureChip = new(35, 35, 50);
    public static readonly Color PastChip = new(18, 18, 28);
    public static readonly Color ChipPartyTint = new(80, 140, 220);
    public static readonly Color ChipEnemyTint = new(220, 80, 80);
    public static readonly Color CurrentText = Color.White;
    public static readonly Color FutureText = new(180, 180, 190);
    public static readonly Color PastText = new(60, 60, 70);

    // Menu colors
    public static readonly Color MenuBg = new Color(10, 10, 25) * 0.92f;
    public static readonly Color MenuItemBg = new Color(20, 20, 40) * 0.5f;
    public static readonly Color MenuItemSelected = new(60, 60, 90);
    public static readonly Color MenuTooltip = new(200, 200, 180);
    public static readonly Color MenuItemNormal = new(180, 180, 190);
    public static readonly Color MenuBackColor = new(180, 180, 100);
    public static readonly Color MenuCannotAfford = new(120, 60, 60);

    // Combat log
    public static readonly Color CombatLogBg = new Color(10, 10, 25) * 0.85f;

    // Divider
    public static readonly Color Divider = new(60, 60, 80);

    // Game over
    public static readonly Color GameOverOverlay = Color.Black * 0.6f;

    // Entity sprite fallback highlight
    public static readonly Color SpriteHighlight = Color.White * 0.2f;

    public static Color RatioToColor(float ratio)
    {
        if (ratio > 0.5f) return HpGreen;
        if (ratio > 0.25f) return HpYellow;
        return HpRed;
    }
}
