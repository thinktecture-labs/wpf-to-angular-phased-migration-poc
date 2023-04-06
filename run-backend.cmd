dotnet build ./Backend/Backend.csproj
cd ./angular-app
CALL npm install
CALL ng build --configuration production
cd ..
XCOPY angular-app\dist\angular-app Backend\bin\Debug\net7.0\angular-app /s /i /y
dotnet run  --project ./Backend/Backend.csproj --no-build