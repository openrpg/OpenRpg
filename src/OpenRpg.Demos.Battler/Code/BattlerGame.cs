using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OpenRpg.Data;
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
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameServices _gameServices;

    public EventHandler OnGameInitialized;

    public BattlerGame(IServiceProvider serviceProvider)
    {
        _graphics = new GraphicsDeviceManager(this);
        _serviceProvider = serviceProvider;
        _gameServices = serviceProvider.GetRequiredService<IGameServices>() as GameServices;
        
        Content.RootDirectory = "Content";
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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
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
    }
}
