cd ./WpfApp
dotnet restore -r win-x64
dotnet build -r win-x64 --no-self-contained --no-restore
cd ./bin/Debug/net7.0-windows/win-x64
CALL WpfApp.exe
cd ../../../../..