using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Demos.Battler.Code;
using OpenRpg.Demos.Battler.Code.Extensions;

var provider = new ServiceCollection()
    .WithOpenRpgFramework()
    .WithOpenRpgProject()
    .WithMonoGameServices()
    .WithSceneServices()
    .BuildServiceProvider();

using var game = new BattlerGame(provider);
game.Run();
