using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;

namespace OpenRpg.Demos.Battler.Code.Scenes;

/// <summary>
/// Shared UI drawing utilities for panels, buttons, borders, and highlights.
/// All scenes should use these for consistent visual styling.
/// </summary>
public static class UiHelper
{
    /// <summary>
    /// Draw a filled rectangle panel with a 1-pixel border.
    /// </summary>
    public static void DrawPanel(SpriteBatch sb, Texture2D pixel, int x, int y, int w, int h, Color fillColor, Color? borderColor = null)
    {
        // Fill
        sb.Draw(pixel, new Rectangle(x, y, w, h), fillColor);

        // Border
        var border = borderColor ?? Color.Lerp(fillColor, Color.White, 0.25f);
        sb.Draw(pixel, new Rectangle(x, y, w, 1), border);                     // top
        sb.Draw(pixel, new Rectangle(x, y + h - 1, w, 1), border);            // bottom
        sb.Draw(pixel, new Rectangle(x, y, 1, h), border);                     // left
        sb.Draw(pixel, new Rectangle(x + w - 1, y, 1, h), border);            // right
    }

    /// <summary>
    /// Draw a button background with border. Selected state has brighter fill and border.
    /// </summary>
    public static void DrawButton(SpriteBatch sb, Texture2D pixel, int x, int y, int w, int h,
        bool isSelected, Color? fillColor = null, Color? selectedFillColor = null,
        Color? borderColor = null, Color? selectedBorderColor = null)
    {
        var fill = isSelected
            ? (selectedFillColor ?? new Color(60, 60, 90))
            : (fillColor ?? new Color(20, 25, 35));
        var border = isSelected
            ? (selectedBorderColor ?? new Color(100, 120, 160))
            : (borderColor ?? new Color(45, 50, 65));

        DrawPanel(sb, pixel, x, y, w, h, fill, border);
    }

    /// <summary>
    /// Draw a highlight rectangle (e.g., for selected list items).
    /// </summary>
    public static void DrawSelectionHighlight(SpriteBatch sb, Texture2D pixel, int x, int y, int w, int h)
    {
        sb.Draw(pixel, new Rectangle(x, y, w, h), new Color(60, 60, 90));
        // Lighter top/left border for a slight 3D inset look
        sb.Draw(pixel, new Rectangle(x, y, w, 1), new Color(90, 100, 140));
        sb.Draw(pixel, new Rectangle(x, y, 1, h), new Color(90, 100, 140));
        sb.Draw(pixel, new Rectangle(x, y + h - 1, w, 1), new Color(35, 35, 55));
        sb.Draw(pixel, new Rectangle(x + w - 1, y, 1, h), new Color(35, 35, 55));
    }

    /// <summary>
    /// Draw a section title bar across the top width of a panel.
    /// </summary>
    public static void DrawTitleBar(SpriteBatch sb, Texture2D pixel, int x, int y, int w, int h,
        Color? fillColor = null, Color? borderColor = null)
    {
        var fill = fillColor ?? new Color(25, 30, 50);
        var border = borderColor ?? new Color(55, 60, 90);
        DrawPanel(sb, pixel, x, y, w, h, fill, border);
    }
}
