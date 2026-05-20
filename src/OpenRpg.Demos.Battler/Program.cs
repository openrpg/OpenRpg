using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Demos.Battler.Code;
using OpenRpg.Demos.Battler.Code.Extensions;

var provider = new ServiceCollection()
    .WithOpenRpgProject()
    .WithMonoGameServices()
    .BuildServiceProvider();

using var game = new BattlerGame(provider);
game.Run();
