using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Services.Game;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class BattleScene : IScene
{
    private readonly IPartyProvider _partyProvider;
    private readonly IEnemyFormationProvider _enemyFormationProvider;
    private readonly IGameServices _gameServices;
    private Texture2D _pixel;
    private Texture2D _hpBarBg;
    private readonly Dictionary<string, Texture2D> _spriteCache = [];
    private BattleBottomPanelUi _bottomPanel;

    public List<BattleEntity> Party { get; private set; } = [];
    public List<BattleEntity> Enemies { get; private set; } = [];

    private static readonly Color BackgroundColor = new(20, 20, 30);
    private static readonly Color PlayerRectColor = new(30, 60, 140);
    private static readonly Color PlayerRectDeadColor = new(20, 25, 40);
    private static readonly Color EnemyRectColor = new(140, 30, 30);
    private static readonly Color EnemyRectDeadColor = new(40, 20, 20);
    private static readonly Color HpGreen = new(80, 200, 60);
    private static readonly Color HpYellow = new(220, 200, 40);
    private static readonly Color HpRed = new(200, 40, 40);
    private static readonly Color HpBgColor = new(30, 30, 30);
    public BattleScene(IPartyProvider partyProvider, IEnemyFormationProvider enemyFormationProvider, IGameServices gameServices)
    {
        _partyProvider = partyProvider;
        _enemyFormationProvider = enemyFormationProvider;
        _gameServices = gameServices;
    }

    public async Task LoadAsync()
    {
        Party = await _partyProvider.BuildPartyAsync();
        Enemies = await _enemyFormationProvider.GenerateFormationAsync();
        LayoutEntities();
        LoadSprites();
        _bottomPanel = new BattleBottomPanelUi();
    }

    public void Unload()
    {
        _bottomPanel.Unload();
        _pixel?.Dispose();
        _hpBarBg?.Dispose();
        foreach (var tex in _spriteCache.Values)
            tex.Dispose();
        _spriteCache.Clear();
    }

    public void Update(GameTime gameTime)
    {
        _bottomPanel.Update(Party, Enemies);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        EnsureTextures(spriteBatch.GraphicsDevice);

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        spriteBatch.Draw(_pixel, new Rectangle(0, 0, 800, 600), BackgroundColor);

        DrawDivider(spriteBatch);

        foreach (var entity in Enemies)
            DrawEntity(spriteBatch, entity);
        foreach (var entity in Party)
            DrawEntity(spriteBatch, entity);

        spriteBatch.End();
    }

    private void EnsureTextures(GraphicsDevice gd)
    {
        if (_pixel != null) return;
        _pixel = new Texture2D(gd, 1, 1);
        _pixel.SetData([Color.White]);
        _hpBarBg = new Texture2D(gd, 1, 1);
        _hpBarBg.SetData([Color.White]);
    }

    private void LoadSprites()
    {
        var content = _gameServices.GetContentManager;

        foreach (var entity in Party.Concat(Enemies))
        {
            if (_spriteCache.TryGetValue(entity.AssetCode, out var tex))
            {
                entity.Sprite = tex;
                continue;
            }

            var subDir = entity.Team == Team.Player ? "Players" : "Enemies";
            var assetPath = $"Sprites/{subDir}/{entity.AssetCode}";

            try
            {
                tex = content.Load<Texture2D>(assetPath);
                _spriteCache[entity.AssetCode] = tex;
                entity.Sprite = tex;
            }
            catch (ContentLoadException)
            {
                System.Diagnostics.Debug.WriteLine($"Missing sprite: {assetPath}");
            }
        }
    }

    private void LayoutEntities()
    {
        foreach (var entity in Enemies)
            entity.Position = new Vector2(30 + entity.SlotInRow * 90, 30 + entity.Row * 130);

        foreach (var entity in Party)
            entity.Position = new Vector2(600 + entity.SlotInRow * 100, 30 + entity.Row * 130);
    }

    private void DrawDivider(SpriteBatch sb)
    {
        sb.Draw(_pixel, new Rectangle(399, 0, 2, 380), new Color(60, 60, 80));
    }

    private void DrawTeamLabel(SpriteBatch sb, string text, int x, int y, Color color)
    {
        var textWidth = text.Length * 9;
        var bg = new Rectangle(x - textWidth / 2 - 10, y, textWidth + 20, 16);
        sb.Draw(_pixel, bg, color * 0.6f);
    }

    private void DrawEntity(SpriteBatch sb, BattleEntity entity)
    {
        if (entity.Sprite != null)
            DrawSprite(sb, entity);
        else
            DrawFallbackRect(sb, entity);

        if (entity.IsAlive)
            DrawHpBar(sb, entity);
    }

    private void DrawSprite(SpriteBatch sb, BattleEntity entity)
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

        sb.Draw(tex, new Rectangle(x, y, w, h), null, entity.IsAlive ? Color.White : Color.Gray * 0.35f, 0f, Vector2.Zero, effects, 0f);
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
}
