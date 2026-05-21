using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Genres.Requirements;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class BattleScene : IScene
{
    private readonly IPartyProvider _partyProvider;
    private readonly IEnemyFormationProvider _enemyFormationProvider;
    private readonly IGameServices _gameServices;
    private readonly ISceneManager _sceneManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDataSource _dataSource;
    private readonly ICharacterRequirementChecker _requirementChecker;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly TurnManager _turnManager;
    private readonly SpriteCache _spriteCache = new();
    private readonly EntityRenderer _entityRenderer = new();

    private BattleBottomPanelUi _bottomPanel;
    private TurnOrderUi _turnOrderUi;
    private CombatLogUi _combatLogUi;
    private CommandMenuUi _commandMenu;
    private KeyboardState _previousKeyboard;
    private double _totalTime;
    private SpriteFont _font;
    private bool _loaded;

    public List<BattleEntity> Party { get; private set; } = [];
    public List<BattleEntity> Enemies { get; private set; } = [];

    public BattleScene(
        IPartyProvider partyProvider,
        IEnemyFormationProvider enemyFormationProvider,
        IGameServices gameServices,
        ISceneManager sceneManager,
        IServiceProvider serviceProvider,
        IDataSource dataSource,
        ICharacterRequirementChecker requirementChecker,
        ILocaleDataSource localeDataSource)
    {
        _partyProvider = partyProvider;
        _enemyFormationProvider = enemyFormationProvider;
        _gameServices = gameServices;
        _sceneManager = sceneManager;
        _serviceProvider = serviceProvider;
        _dataSource = dataSource;
        _requirementChecker = requirementChecker;
        _localeDataSource = localeDataSource;
        _turnManager = new TurnManager(_dataSource, _requirementChecker, _localeDataSource);
    }

    public async Task LoadAsync()
    {
        try
        {
            _loaded = false;

            Party = await _partyProvider.BuildPartyAsync();
            Enemies = await _enemyFormationProvider.GenerateFormationAsync();
            LayoutEntities();

            foreach (var e in Party.Concat(Enemies))
                e.OriginPosition = e.Position;

            var content = _gameServices.GetContentManager;
            _spriteCache.LoadSprites(Party.Concat(Enemies), content);
            _turnManager.Start(Party, Enemies);

            _font = content.Load<SpriteFont>("Fonts/KenneyPixel");
            _turnOrderUi = new TurnOrderUi(_font);
            _bottomPanel = new BattleBottomPanelUi();
            _bottomPanel.Update(Party, Enemies);
            _turnOrderUi.Update(_turnManager.TurnOrder, _turnManager.CurrentTurnIndex);
            _combatLogUi = new CombatLogUi();
            _combatLogUi.Update("");

            _commandMenu = new CommandMenuUi();
            _commandMenu.OnActionConfirmed += OnPlayerActionConfirmed;

            _loaded = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BattleScene] FAILED TO LOAD: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    public void Unload()
    {
        _commandMenu?.Hide();
        _turnOrderUi.Unload();
        _bottomPanel.Unload();
        _combatLogUi.Unload();
        _entityRenderer.Dispose();
        _font = null;
    }

    public void Update(GameTime gameTime)
    {
        if (!_loaded) return;
        _totalTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_turnManager.CurrentPhase == TurnManager.Phase.GameOver)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space) || kstate.IsKeyDown(Keys.Enter))
            {
                var newScene = _serviceProvider.GetRequiredService<BattleScene>();
                _ = _sceneManager.SetScene(newScene);
            }
            return;
        }

        AnimateEntities();
        _turnManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

        if (_turnManager.IsInPlayerInput)
        {
            var currentKeyboard = Keyboard.GetState();

            if (_commandMenu.IsHidden)
            {
                var aliveEnemies = Enemies.Where(e => e.IsAlive).ToList();
                var abilities = _turnManager.GetAvailableAbilities(_turnManager.CurrentAttacker);
                _commandMenu.Show(_turnManager.CurrentAttacker, aliveEnemies, abilities, _localeDataSource);
            }

            _commandMenu.HandleInput(currentKeyboard, _previousKeyboard);
            _previousKeyboard = currentKeyboard;

            // Sync highlighted targets from menu selection to turn manager for visual feedback
            _turnManager.HighlightedTargets = _commandMenu.PreviewTargets;
        }
        else if (!_commandMenu.IsHidden)
        {
            _commandMenu.Hide();
            _turnManager.HighlightedTargets = [];
        }

        _bottomPanel.Update(Party, Enemies);
        _turnOrderUi.Update(_turnManager.TurnOrder, _turnManager.CurrentTurnIndex, GetPulseBrightness());
        _combatLogUi.Update(_turnManager.LastActionMessage);
    }

    private void OnPlayerActionConfirmed(PlayerAction action)
    {
        _turnManager.SubmitPlayerAction(action);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;
        _entityRenderer.EnsureTextures(spriteBatch.GraphicsDevice);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _entityRenderer.DrawBackground(spriteBatch);
        _entityRenderer.DrawEntities(spriteBatch, Enemies, _turnManager, _totalTime);
        _entityRenderer.DrawEntities(spriteBatch, Party, _turnManager, _totalTime);

        spriteBatch.End();
    }

    public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_loaded) return;
        _combatLogUi.Draw(spriteBatch, _font);
        _turnOrderUi.Draw(spriteBatch);
        _bottomPanel.Draw(spriteBatch, _font);

        _commandMenu.Draw(spriteBatch, _font);

        if (_turnManager.CurrentPhase == TurnManager.Phase.GameOver)
            _entityRenderer.DrawGameOver(spriteBatch, _font, _turnManager.WinningTeam);
    }

    private float GetPulseBrightness()
    {
        if (_turnManager.CurrentPhase != TurnManager.Phase.TurnDwell &&
            _turnManager.CurrentPhase != TurnManager.Phase.PlayerInput) return 0;
        return (float)(0.5 + Math.Sin(_totalTime * 10) * 0.5);
    }

    private void AnimateEntities()
    {
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
    }

    private void LayoutEntities()
    {
        foreach (var entity in Enemies)
            entity.Position = new Vector2(30 + entity.SlotInRow * 90, 30 + entity.Row * 100);

        foreach (var entity in Party)
            entity.Position = new Vector2(600 + entity.SlotInRow * 100, 30 + entity.Row * 100);
    }
}
