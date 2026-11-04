# Console report format example generation

These Cake tasks generate the console output screenshots shown in the Console report format documentation. They live with the Console integration tests and follow the report-generation layout used by `Cake.Issues.Reporting.Generic`.

## Regenerate all screenshots

From the repository root, create the local NuGet packages and run the screenshot generator:

```bash
./build.sh --target=Create-NuGet-Packages
cd tests/Cake.Issues.Reporting.Console/script-runner/net8.0
./generate-screenshots.sh
```

On Windows, create the packages with `.\build.ps1 --target=Create-NuGet-Packages`; run the screenshot generator in a Linux environment with the listed tools installed.

The script requires `dotnet`, util-linux `script`, `ansi2html`, and `wkhtmltoimage` (provided by the `wkhtmltopdf` package on Ubuntu). It runs the Cake test runner against the local packages and writes the PNGs to `docs/input/assets/images/console-examples/`.

The tasks and the shared sample data are in this directory. They can also be run individually with `dotnet cake build.cake --target=<target>` from the `net8.0` runner directory, using one of these targets: `Create-Reports-Console-Default`, `Create-Reports-Console-Grouped`, `Create-Reports-Console-Summaries`, or `Create-Reports-Console-Combined`.
