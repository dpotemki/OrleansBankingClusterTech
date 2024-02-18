@echo off
SETLOCAL EnableDelayedExpansion

REM build all solution in folder \src
cd src
dotnet build
cd ..

REM run first Silo
start cmd /k "cd src\Silo && dotnet run"

REM pause on few seconds
timeout /t 2 /nobreak

REM Run second Silo
start cmd /k "cd src\Silo && dotnet run"

REM run api ApiClient
start cmd /k "cd src\ApiClient && dotnet run"

REM back to root
cd ..

ENDLOCAL
