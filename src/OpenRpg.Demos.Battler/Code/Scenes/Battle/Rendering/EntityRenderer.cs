using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Rendering;

public class EntityRenderer
{
    private Texture2D _pixel;

    public void EnsureTextures(GraphicsDevice gd)
    {
        if (_pixel != null) return;
        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData([Color.White]);
    }

    public void DrawBackground(SpriteBatch sb)
    {
        sb.Draw(_pixel, new Rectangle(0, 0, 800, 600), Palette.Background);
        sb.Draw(_pixel, new Rectangle(399, 0, 2, 344), Palette.Divider);
    }

    public void DrawEntities(SpriteBatch sb, List<BattleEntity> entities, TurnManager turnManager, double totalTime)
    {
        foreach (var entity in entities)
            DrawEntity(sb, entity, turnManager, totalTime);
    }

    public void DrawOverlay(SpriteBatch sb)
    {
        sb.Draw(_pixel, new Rectangle(0, 0, 800, 600), Palette.GameOverOverlay);
    }

    public void Dispose()
    {
        _pixel?.Dispose();
    }

    private void DrawEntity(SpriteBatch sb, BattleEntity entity, TurnManager turnManager, double totalTime)
    {
        if (entity.Sprite != null)
            DrawSprite(sb, entity, turnManager, totalTime);
        else
            DrawFallbackRect(sb, entity);

        if (entity.IsAlive)
            DrawHpBar(sb, entity);

        if ((turnManager.CurrentPhase == TurnManager.Phase.TurnDwell || turnManager.CurrentPhase == TurnManager.Phase.PlayerInput) && entity == turnManager.CurrentAttacker)
            DrawTurnArrow(sb, entity, totalTime, Color.Gold);

        if (turnManager.HighlightedTargets?.Contains(entity) == true)
            DrawTurnArrow(sb, entity, totalTime, Color.Red);
    }

    private static void DrawSprite(SpriteBatch sb, BattleEntity entity, TurnManager turnManager, double totalTime)
    {
        var tex = entity.Sprite;
        var slotW = 80;
        var slotH = 60;
        var scale = Math.Min((float)slotW / tex.Width, (float)slotH / tex.Height);
        var w = (int)(tex.Width * scale);
        var h = (int)(tex.Height * scale);
        var x = (int)entity.Position.X + (slotW - w) / 2;
        var y = (int)entity.Position.Y + (slotH - h) / 2;
        var effects = entity.Team == Team.Player ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        var color = entity.IsAlive ? Color.White : Color.Gray * 0.35f;
        sb.Draw(tex, new Rectangle(x, y, w, h), null, color, 0f, Vector2.Zero, effects, 0f);

        if (turnManager.CurrentPhase == TurnManager.Phase.TargetFlash && turnManager.CurrentTargets?.Contains(entity) == true)
            sb.Draw(tex, new Rectangle(x, y, w, h), null, Color.Red * 0.5f, 0f, Vector2.Zero, effects, 0f);
    }

    private void DrawFallbackRect(SpriteBatch sb, BattleEntity entity)
    {
        var rect = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, 80, 60);
        var color = entity.IsAlive
            ? (entity.Team == Team.Player ? Palette.PlayerRect : Palette.EnemyRect)
            : (entity.Team == Team.Player ? Palette.PlayerRectDead : Palette.EnemyRectDead);

        sb.Draw(_pixel, rect, color);

        if (entity.IsAlive)
            sb.Draw(_pixel, new Rectangle(rect.X, rect.Y, rect.Width, 2), Palette.SpriteHighlight);
    }

    private void DrawHpBar(SpriteBatch sb, BattleEntity entity)
    {
        var barX = (int)entity.Position.X;
        var barY = (int)entity.Position.Y + 64;
        var barWidth = 80;
        var barHeight = 5;

        sb.Draw(_pixel, new Rectangle(barX, barY, barWidth, barHeight), Palette.HpBarBg);

        var ratio = (float)entity.Hp / entity.MaxHp;
        var fillWidth = (int)(barWidth * ratio);
        if (fillWidth > 0)
            sb.Draw(_pixel, new Rectangle(barX, barY, fillWidth, barHeight), Palette.RatioToColor(ratio));
    }

    private void DrawTurnArrow(SpriteBatch sb, BattleEntity entity, double totalTime, Color color)
    {
        var slotW = 80;
        var tweenY = Math.Sin(totalTime * 6) * 4;
        var cx = (int)(entity.Position.X + slotW / 2);
        var baseY = (int)(entity.Position.Y - 18 + tweenY);

        sb.Draw(_pixel, new Rectangle(cx - 4, baseY, 9, 2), color);
        sb.Draw(_pixel, new Rectangle(cx - 3, baseY + 3, 7, 2), color);
        sb.Draw(_pixel, new Rectangle(cx - 2, baseY + 6, 5, 2), color);
        sb.Draw(_pixel, new Rectangle(cx - 1, baseY + 9, 3, 2), color);
    }
}
