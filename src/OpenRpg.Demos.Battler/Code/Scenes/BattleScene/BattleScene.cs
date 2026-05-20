using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Services.Game;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class BattleScene : IScene
{
    private readonly IPartyProvider _partyProvider;
    private readonly IEnemyFormationProvider _enemyFormationProvider;
    private readonly IGameServices _gameServices;
    private readonly ISceneManager _sceneManager;
    private readonly IServiceProvider _serviceProvider;
    private Texture2D _pixel;
    private Texture2D _hpBarBg;
    private readonly Dictionary<string, Texture2D> _spriteCache = [];
    private BattleBottomPanelUi _bottomPanel;
    private TurnOrderUi _turnOrderUi;
    private List<BattleEntity> _turnOrder;
    private int _currentTurnIndex;
    private double _totalTime;
    private SpriteFont _font;
    private static readonly Random _rng = new();

    private enum TurnPhase { Idle, TurnDwell, TargetFlash, GameOver }
    private TurnPhase _currentPhase = TurnPhase.Idle;
    private double _phaseTimer;
    private BattleEntity _currentAttacker;
    private BattleEntity _currentTarget;
    private Team _winningTeam;

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

    public BattleScene(
        IPartyProvider partyProvider,
        IEnemyFormationProvider enemyFormationProvider,
        IGameServices gameServices,
        ISceneManager sceneManager,
        IServiceProvider serviceProvider)
    {
        _partyProvider = partyProvider;
        _enemyFormationProvider = enemyFormationProvider;
        _gameServices = gameServices;
        _sceneManager = sceneManager;
        _serviceProvider = serviceProvider;
    }

    public async Task LoadAsync()
    {
        Party = await _partyProvider.BuildPartyAsync();
        Enemies = await _enemyFormationProvider.GenerateFormationAsync();
        LayoutEntities();
        foreach (var e in Party.Concat(Enemies))
            e.OriginPosition = e.Position;
        LoadSprites();
        _turnOrder = Party.Concat(Enemies).OrderByDescending(e => e.Initiative).ToList();
        _currentTurnIndex = -1;
        _currentPhase = TurnPhase.Idle;
        _phaseTimer = 0;
        _totalTime = 0;

        var content = _gameServices.GetContentManager;
        _font = content.Load<SpriteFont>("Fonts/KenneyPixel");
        _turnOrderUi = new TurnOrderUi(_font);
        _bottomPanel = new BattleBottomPanelUi();
    }

    public void Unload()
    {
        _turnOrderUi.Unload();
        _bottomPanel.Unload();
        _pixel?.Dispose();
        _hpBarBg?.Dispose();
        _font = null;
        foreach (var tex in _spriteCache.Values)
            tex.Dispose();
        _spriteCache.Clear();
    }

    public void Update(GameTime gameTime)
    {
        _totalTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_currentPhase == TurnPhase.GameOver)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space) || kstate.IsKeyDown(Keys.Enter))
            {
                var newScene = _serviceProvider.GetRequiredService<BattleScene>();
                _ = _sceneManager.SetScene(newScene);
            }
            return;
        }

        for (var i = 0; i < Enemies.Count; i++)
        {
            var e = Enemies[i];
            if (!e.IsAlive) continue;
            var phase = i * 1.3;
            var offX = Math.Sin(_totalTime * 1.2 + phase) * 2.0;
            var offY = Math.Cos(_totalTime * 0.9 + phase) * 1.5;
            e.Position = e.OriginPosition + new Vector2((float)offX, (float)offY);
        }

        for (var i = 0; i < Party.Count; i++)
        {
            var e = Party[i];
            if (!e.IsAlive) continue;
            var phase = i * 1.7 + 3.0;
            var offX = Math.Sin(_totalTime * 1.0 + phase) * 2.0;
            var offY = Math.Cos(_totalTime * 1.1 + phase) * 1.5;
            e.Position = e.OriginPosition + new Vector2((float)offX, (float)offY);
        }

        _phaseTimer += gameTime.ElapsedGameTime.TotalSeconds;

        switch (_currentPhase)
        {
            case TurnPhase.Idle:
                if (AdvanceTurn())
                {
                    _currentPhase = TurnPhase.TurnDwell;
                    _phaseTimer = 0;
                }
                break;

            case TurnPhase.TurnDwell:
                if (_phaseTimer >= 0.8)
                {
                    ApplyDamage();
                    _currentPhase = TurnPhase.TargetFlash;
                    _phaseTimer = 0;
                }
                break;

            case TurnPhase.TargetFlash:
                if (_phaseTimer >= 0.3)
                {
                    if (CheckGameOver())
                        _currentPhase = TurnPhase.GameOver;
                    else
                    {
                        _currentPhase = TurnPhase.Idle;
                        _phaseTimer = 0;
                    }
                }
                break;
        }

        _bottomPanel.Update(Party, Enemies);
        _turnOrderUi.Update(_turnOrder, _currentTurnIndex, GetPulseBrightness());
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

    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _turnOrderUi.Draw(spriteBatch);
        _bottomPanel.Draw(spriteBatch, _font);

        if (_currentPhase == TurnPhase.GameOver)
        {
            spriteBatch.Draw(_pixel, new Rectangle(0, 0, 800, 600), Color.Black * 0.6f);

            var winText = _winningTeam == Team.Player ? "Player Wins!" : "Monsters Win!";
            var winSize = _font.MeasureString(winText);
            spriteBatch.DrawString(_font, winText,
                new Vector2(400 - winSize.X / 2, 250), Color.Gold);

            var restartText = "Press SPACE to try again";
            var restartSize = _font.MeasureString(restartText);
            spriteBatch.DrawString(_font, restartText,
                new Vector2(400 - restartSize.X / 2, 300), Color.White);
        }
    }

    private float GetPulseBrightness()
    {
        if (_currentPhase != TurnPhase.TurnDwell) return 0;
        return (float)(0.5 + Math.Sin(_totalTime * 10) * 0.5);
    }

    private bool AdvanceTurn()
    {
        var count = _turnOrder.Count;
        for (var i = 0; i < count; i++)
        {
            _currentTurnIndex = (_currentTurnIndex + 1) % count;
            _currentAttacker = _turnOrder[_currentTurnIndex];
            if (_currentAttacker.IsAlive)
            {
                var targets = _currentAttacker.Team == Team.Player ? Enemies : Party;
                var aliveTargets = targets.Where(e => e.IsAlive).ToList();
                if (aliveTargets.Count == 0) continue;
                _currentTarget = aliveTargets[_rng.Next(aliveTargets.Count)];
                return true;
            }
        }
        return false;
    }

    private void ApplyDamage()
    {
        var damage = _currentAttacker.AttackDamage > 0
            ? _currentAttacker.AttackDamage
            : _rng.Next(5, 15);
        _currentTarget.Hp = Math.Max(0, _currentTarget.Hp - damage);
    }

    private bool CheckGameOver()
    {
        if (Party.All(e => !e.IsAlive))
        {
            _winningTeam = Team.Enemy;
            return true;
        }
        if (Enemies.All(e => !e.IsAlive))
        {
            _winningTeam = Team.Player;
            return true;
        }
        return false;
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
            entity.Position = new Vector2(30 + entity.SlotInRow * 90, 30 + entity.Row * 100);

        foreach (var entity in Party)
            entity.Position = new Vector2(600 + entity.SlotInRow * 100, 30 + entity.Row * 100);
    }

    private void DrawDivider(SpriteBatch sb)
    {
        sb.Draw(_pixel, new Rectangle(399, 0, 2, 344), new Color(60, 60, 80));
    }

    private void DrawEntity(SpriteBatch sb, BattleEntity entity)
    {
        if (entity.Sprite != null)
            DrawSprite(sb, entity);
        else
            DrawFallbackRect(sb, entity);

        if (entity.IsAlive)
            DrawHpBar(sb, entity);

        if (_currentPhase == TurnPhase.TurnDwell && entity == _currentAttacker)
            DrawTurnArrow(sb, entity);
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

        var color = entity.IsAlive ? Color.White : Color.Gray * 0.35f;
        sb.Draw(tex, new Rectangle(x, y, w, h), null, color, 0f, Vector2.Zero, effects, 0f);

        if (_currentPhase == TurnPhase.TargetFlash && entity == _currentTarget)
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

    private void DrawTurnArrow(SpriteBatch sb, BattleEntity entity)
    {
        var slotW = 80;
        var tweenY = Math.Sin(_totalTime * 6) * 4;
        var cx = (int)(entity.Position.X + slotW / 2);
        var baseY = (int)(entity.Position.Y - 18 + tweenY);

        sb.Draw(_pixel, new Rectangle(cx - 4, baseY, 9, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 3, baseY + 3, 7, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 2, baseY + 6, 5, 2), Color.Gold);
        sb.Draw(_pixel, new Rectangle(cx - 1, baseY + 9, 3, 2), Color.Gold);
    }
}
