const { spawnSync } = require("node:child_process");
const { existsSync, mkdtempSync, readFileSync, rmSync, writeFileSync } = require("node:fs");
const os = require("node:os");
const path = require("node:path");
const AnsiToHtml = require("ansi-to-html");
const { chromium } = require("playwright-core");

const runner = __dirname;
const imageDirectory = path.resolve(runner, "../../../../docs/input/assets/images/console-examples");
const examples = [
  ["Default", "console-default.png"],
  ["Grouped", "console-grouped.png"],
  ["Summaries", "console-summaries.png"],
  ["Combined", "console-combined.png"],
];

function runDotnet(args) {
  const result = spawnSync("dotnet", args, { cwd: runner, stdio: "inherit" });
  if (result.error) {
    throw result.error;
  }
  if (result.status !== 0) {
    throw new Error(`dotnet ${args.join(" ")} failed with exit code ${result.status}`);
  }
}

function browserPath() {
  const candidates = [
    process.env.CONSOLE_SCREENSHOT_BROWSER,
    process.env.PROGRAMFILES && path.join(process.env.PROGRAMFILES, "Google/Chrome/Application/chrome.exe"),
    process.env["PROGRAMFILES(X86)"] && path.join(process.env["PROGRAMFILES(X86)"], "Microsoft/Edge/Application/msedge.exe"),
    "/usr/bin/google-chrome",
    "/usr/bin/chromium",
    "/usr/bin/chromium-browser",
    "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
    "/Applications/Microsoft Edge.app/Contents/MacOS/Microsoft Edge",
  ];
  const browser = candidates.find((candidate) => candidate && existsSync(candidate));
  if (!browser) {
    throw new Error("Install Chrome, Chromium, or Edge, or set CONSOLE_SCREENSHOT_BROWSER to its executable.");
  }
  return browser;
}

async function main() {
  const temporaryDirectory = mkdtempSync(path.join(os.tmpdir(), "cake-console-screenshots-"));
  let browser;
  try {
    runDotnet(["tool", "restore"]);
    runDotnet(["cake", "build.cake", "--bootstrap"]);
    browser = await chromium.launch({ executablePath: browserPath(), headless: true });
    const page = await browser.newPage({ viewport: { width: 1400, height: 900 }, deviceScaleFactor: 1 });
    const converter = new AnsiToHtml({
      fg: "#dedede",
      bg: "#1e1e1e",
      colors: { 1: "#f87171", 2: "#4ade80", 4: "#82aaff", 8: "#999999", 12: "#a6c8ff" },
      escapeXML: true,
    });
    const screenshots = [];

    for (const [name, image] of examples) {
      const report = path.join(temporaryDirectory, `${name}.ansi`);
      runDotnet([
        "cake", "build.cake", `--target=Create-Reports-Console-${name}`,
        "--settings_skippackageversioncheck=true", `--report-output=${report}`,
      ]);
      const output = readFileSync(report, "utf8");
      if (!output.includes("CS0219") || !output.includes("CA1822") ||
          /Errata Error|An error occurred when rendering a diagnostic|Exception/i.test(output)) {
        throw new Error(`${name} report did not render valid diagnostics.`);
      }
      const cleanOutput = output.replace(/ +(?=(?:\x1b\[[0-9;]*m)*\r?$)/gm, "").trimEnd();
      const html = converter.toHtml(cleanOutput);
      await page.setContent(`<!doctype html><meta charset="utf-8"><style>
        html, body { margin: 0; background: #1e1e1e; }
        pre { display: inline-block; box-sizing: border-box; margin: 0; padding: 8px;
              background: #1e1e1e; color: #dedede; font: 16px/1.3 Consolas, "DejaVu Sans Mono", monospace;
              white-space: pre; }
      </style><pre>${html}</pre>`);
      screenshots.push([image, await page.locator("pre").screenshot({ animations: "disabled" })]);
    }

    for (const [image, data] of screenshots) {
      writeFileSync(path.join(imageDirectory, image), data);
      console.log(`Generated ${image}`);
    }
  } finally {
    if (browser) {
      await browser.close();
    }
    rmSync(temporaryDirectory, { recursive: true, force: true });
  }
}

main().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
