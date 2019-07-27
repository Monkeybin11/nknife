echo "Start building Electron.NET dev stack..."
echo "NKnife"
cd NKnife
dotnet restore
dotnet build
cd ..
echo "NKnife"
cd NKnife.UnitTests
dotnet test
cd ..

:: Be aware, that for non-electronnet-dev environments the correct 
:: invoke command would be dotnet electronize ...

:: Not supported on Windows Systems, because of SymLinks...
:: echo "/target osx"
::   dotnet electronize build /target osx
