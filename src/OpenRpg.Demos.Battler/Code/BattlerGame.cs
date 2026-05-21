using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gum.Wireframe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Scenes.Battle;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Entity.Templates;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Loaders;

namespace OpenRpg.Demos.Battler.Code;

public class BattlerGame : Game
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISceneManager _sceneManager;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameServices _gameServices;
    private volatile bool _projectLoaded;

    public BattlerGame(IServiceProvider serviceProvider)
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();
        _serviceProvider = serviceProvider;
        _gameServices = serviceProvider.GetRequiredService<IGameServices>() as GameServices;
        _sceneManager = serviceProvider.GetRequiredService<ISceneManager>();
        
        Content.RootDirectory = "Content";
        _gameServices.GetContentManager = Content;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _gameServices.GetSpriteBatch = _spriteBatch;
        _gameServices.GetGraphicsDeviceManager = _graphics;
        
        Task.Run(LoadProjectData);
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gameServices.GetSpriteBatch = _spriteBatch;

        GumService.Default.Initialize(this);
        GraphicalUiElement.CanvasWidth = 800;
        GraphicalUiElement.CanvasHeight = 600;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_projectLoaded && _sceneManager.ActiveScene == null)
        {
            var battleScene = _serviceProvider.GetRequiredService<BattleScene>();
            _ = _sceneManager.SetScene(battleScene);
        }

        _sceneManager.Update(gameTime);
        GumService.Default.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        if (_projectLoaded)
            _sceneManager.Draw(gameTime, _spriteBatch);

        RenderingLibrary.SystemManagers.Default.Draw();

        if (_projectLoaded)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _sceneManager.DrawUI(gameTime, _spriteBatch);
            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }

    private async Task LoadProjectData()
    {
        try
        {
            var dataLoader = _serviceProvider.GetRequiredService<IDataLoader>();
            var projectPath = Path.Combine(AppContext.BaseDirectory, @"Content/Project/project.json");
            var project = await dataLoader.Load(projectPath);
            var dataSource = _serviceProvider.GetRequiredService<IDataSource>();

            var items = dataSource.GetAll<ItemTemplate>().Count();
            var monsters = dataSource.GetAll<EntityTemplate>().Count();
            var races = dataSource.GetAll<RaceTemplate>().Count();
            var classes = dataSource.GetAll<ClassTemplate>().Count();
            var localeDataSource = _serviceProvider.GetRequiredService<ILocaleDataSource>();
            var locales = string.Join(", ", localeDataSource.GetLocaleCodes());

            Console.WriteLine("");
            Console.WriteLine("=== OpenRpg Battler Demo ===");
            Console.WriteLine($"Project loaded: v{project.Version} [{project.Type}]");
            Console.WriteLine($"Items:     {items}");
            Console.WriteLine($"Monsters:  {monsters}");
            Console.WriteLine($"Races:     {races}");
            Console.WriteLine($"Classes:   {classes}");
            Console.WriteLine($"Locales:   {locales}");
            Console.WriteLine("");
            Console.WriteLine("OpenRpg services initialized successfully.");
            Console.WriteLine("");
        }
        catch (Exception ex)
        {
            Console.WriteLine("");
            Console.WriteLine("=== FAILED TO LOAD PROJECT ===");
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack: {ex.StackTrace}");
            Console.WriteLine("");
        }

        _projectLoaded = true;
    }
}
