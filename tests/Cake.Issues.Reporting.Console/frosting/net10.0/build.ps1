$RECIPE_PACKAGE_PATH = "packages/cake.issues.reporting.console"
if (Test-Path $RECIPE_PACKAGE_PATH)
{
    Write-Host "Cleaning up cached version of $RECIPE_PACKAGE_PATH..."
    Remove-Item $RECIPE_PACKAGE_PATH -Recurse;
}
else
{
    Write-Host "$RECIPE_PACKAGE_PATH not cached..."
}

dotnet run --project build/Build.csproj -- $args
exit $LASTEXITCODE;