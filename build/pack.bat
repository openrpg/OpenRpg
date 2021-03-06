set version=0.7.20
dotnet pack ../src/OpenRpg.Core -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Items -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Combat -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Localization -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Data -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Quests -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Genres.Fantasy -c Release -o ../../_dist /p:version=%version%