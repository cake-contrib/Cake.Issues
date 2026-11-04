
$RECIPE_PACKAGE_PATH = "packages/cake.issues.reporting.console"
if [ -d "$RECIPE_PACKAGE_PATH" ]
then
    echo "Cleaning up cached version of $RECIPE_PACKAGE_PATH..."
    rm -Rf $RECIPE_PACKAGE_PATH
else
    echo "$RECIPE_PACKAGE_PATH not cached..."
fi

dotnet run --project ./build/Build.csproj -- "$@"
