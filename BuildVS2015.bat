ECHO OFF

:: restore NuGet packages for Web-project
CALL nuget restore MoneyKeeper.sln

:: build server-side application
SET DEVENV="C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe"
IF NOT EXIST %DEVENV% GOTO :EOF

ECHO %DEVENV%

CALL %DEVENV% MoneyKeeper.sln /rebuild

:: restore client libs
CD MoneyKeeper.Web
CALL npm install

:: build client-side application
CALL npm run build

PAUSE