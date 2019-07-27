set ENETVER=19.0.3
echo "Start building NKnife..."
cd NKnife
dotnet restore
dotnet build --configuration Release --force /property:Version=%ENETVER%
dotnet pack /p:Version=%ENETVER% --configuration Release --force --output "%~dp0artifacts"
cd ..

dotnet nuget push ./artifacts/NKnife.%ENETVER%.nupkg -s https://api.nuget.org/v3/index.json -k oy2hdyudzgmfgmrur5cqhdxklkrvunpuleh2atfkqmgbwu