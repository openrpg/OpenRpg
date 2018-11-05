set version=0.1.0
dotnet pack ../src/OpenRpg.Core -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Items -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Localization -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/OpenRpg.Data -c Release -o ../../_dist /p:version=%version%