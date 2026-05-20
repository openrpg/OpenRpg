using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class EntityRenderer
{
    private Texture2D _pixel;
    private Texture2D _hpBarBg;

    private static readonly Color BackgroundColor = new(20, 20, 30);
    private static readonly Color PlayerRectColor = new(30, 60, 140);
    private static readonly Color PlayerRectDeadColor = new(20, 25, 40);
    private static readonly Color EnemyRectColor = new(140, 30, 30);
    private static readonly Color EnemyRectDeadColor = new(40, 20, 20);
    private static readonly Color HpGreen = new(80, 200, 60);
    private static readonly Color HpYellow = new(220, 200, 40);
    private static readonly Color HpRed = new(200, 40, 40);
    private static readonly Color HpBgColor = new(30, 30, 30);

    public void EnsureTextures(GraphicsDevice gd)
    {
        if (_pixel != null) return;
        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData([Color.White]);
        _hpBarBg = new Texture2D(gd, 1, 1);
        _hpBarBg.SetData([Color.White]);
    }

    public void DrawBackground(SpriteBatch sb)
    {
        sb.Draw(_pixel, new Rectangle(0, 0, 800, 600), BackgroundColor);
        sb.Draw(_pixel, new Rectangle(399, 0, 2, 344), new Color(60, 60, 80));
    }

    public void DrawEntities(SpriteBatch sb, List<BattleEntity> entities, TurnManager turnManager, double totalTime)
    {
        foreach (var entity in entities)
            DrawEntity(sb, entity, turnManager, totalTime);
    }

    public void DrawGameOver(SpriteBatch sb, SpriteFont font, Team winningTeam)
    {
        sb.Draw(_pixel, new Rectangle(0, 0, 800, 600), Color.Black * 0.6f);

        var winText = winningTeam == Team.Player ? "Player Wins!" : "Monsters Win!";
        var winSize = font.MeasureString(winText);
        TextHelper.DrawStringWithSpacing(sb, font, winText,
            new Vector2(400 - winSize.X / 2, 250), Color.Gold);

        var restartText = "Press SPACE to try again";
        var restartSize = font.MeasureString(restartText);
        TextHelper.DrawStringWithSpacing(sb, font, restartText,
            new Vector2(400 - restartSize.X / 2, 300), Color.White);
    }

    public void Dispose()
    {
        _pixel?.Dispose();
        _hpBarBg?.Dispose();
    }

    private void DrawEntity(SpriteBatch sb, BattleEntity entity, TurnManager turnManager, double totalTime)
    {
        if (entity.Sprite != null)
            DrawSprite(sb, entity, turnManager);
        else
            DrawFallbackRect(sb, entity);

        if (entity.IsAlive)
            DrawHpBar(sb, entity);

        if (turnManager.CurrentPhase == TurnManager.Phase.TurnDwell && entity == turnManager.CurrentAttacker)
            DrawTurnArrow(sb, entity, totalTime);
    }

    private void DrawSprite(SpriteBatch sb, BattleEntity entity, TurnManager turnManager)
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

        if (turnManager.CurrentPhase == TurnManager.Phase.TargetFlash && entity == turnManager.CurrentTarget)
            sb.Draw(tex, new Rectangle(x, y, w, h), null, Color.Red * 0.5f, 0f, Vector2.Zero, effects, 0f);
    }

    private void DrawFallbackRect(SpriteBatch sb, BattleEntity entity)
    {
        var rect = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, 80, 60);
        var color = entity.IsAlive
            ? (entity.Team == Team.Player ? PlayerRectColor : EnemyRectColor)
            : (entity.Team == Team.Player ? PlayerRectDeadColor : EnemyRectDeadColor);

        sb.Draw(_pixel, rect, color);

        if (entity.IsAlive)
            sb.Draw(_pixel, new Rectangle(rect.X, rect.Y, rect.Width, 2), Color.White * 0.2f);
    }

    private void DrawHpBar(SpriteBatch sb, BattleEntity entity)
    {
        var barX = (int)entity.Position.X;
        var barY = (int)entity.Position.Y + 64;
        var barWidth = 80;
        var barHeight = 5;

        sb.Draw(_hpBarBg, new Rectangle(barX, barY, barWidth, barHeight), HpBgColor);

        var ratio = (float)entity.Hp / entity.MaxHp;
        var fillWidth = (int)(barWidth * ratio);
        if (fillWidth > 0)
        {
            var fillColor = ratio > 0.5f ? HpGreen : (ratio > 0.25f ? HpYellow : HpRed);
            sb.Draw(_pixel, new Rectangle(barX, barY, fillWidth, barHeight), fillColor);
        }
    }

    private void DrawTurnArrow(SpriteBatch sb, BattleEntity entity, double totalTime)
    {
        var slotW = 80;
        var tweenY = Math.Sin(totalTime * 6) * 4;
        var cx = (int)(entity.Position.X + slotW / 2);
        var baseY = (int)(entity.Position.Y - 18 + tweenY);

        sb.Draw(_pixel, new Rectangle(cx - 4, baseY, 9, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 3, baseY + 3, 7, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 2, baseY + 6, 5, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 1, baseY + 9, 3, 2), Color.Gold);
    }
}
