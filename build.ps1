$ErrorActionPreference = 'Stop'

Write-Host "Running Build"
dotnet run --project "$PSScriptRoot/build/Build.csproj" -- @args
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }