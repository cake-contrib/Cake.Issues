# Console report format example generation

The Console integration test runs the four Cake tasks in this directory using
`sample-issues.json` and the checked-in source files under `repository`. The
fixture locations are validated before each report is rendered, and Errata
rendering errors fail the test rather than being included in a screenshot.

To regenerate all screenshots, create local NuGet packages from the repository
root, then install the renderer dependencies and run the generator:

```bash
./build.sh --target=Create-NuGet-Packages
cd tests/Cake.Issues.Reporting.Console/script-runner/net8.0
npm ci
npm run screenshots
```

On Windows, use `.\build.ps1 --target=Create-NuGet-Packages` for the first
command. Node.js and a local Chrome, Chromium, or Microsoft Edge installation
are required. If the browser is not in a standard location, set
`CONSOLE_SCREENSHOT_BROWSER` to its executable path.

The generator captures ANSI output directly from the real Console addin,
checks every report, and uses the browser to crop each PNG to the console
content. It writes all four PNGs to `docs/input/assets/images/console-examples/`
only after all reports and screenshots succeed.
