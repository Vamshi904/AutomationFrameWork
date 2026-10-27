const assert = require("assert");
const fs = require("fs");
const os = require("os");
const path = require("path");
const {
  generateCSharpCode,
  parseRecordedActions,
  validateGeneratedCSharp
} = require("./CSharpCodeGenerator.cjs");

function withTempFiles(run) {
  const tempDir = fs.mkdtempSync(path.join(os.tmpdir(), "csharp-codegen-"));
  try {
    run(tempDir);
  } finally {
    fs.rmSync(tempDir, { recursive: true, force: true });
  }
}

function writeAppSettings(tempDir) {
  fs.writeFileSync(path.join(tempDir, "appsettings.json"), JSON.stringify({
    Urls: {
      DocAdmin: "https://documentadministration-sit.trimont.com/adminReassign.aspx"
    }
  }, null, 2), "utf8");
}

function testClickMapping() {
  const code = "await page.getByRole('button', { name: 'Login' }).click();";
  const actions = parseRecordedActions(code);
  assert.strictEqual(actions.length, 1);
  assert.strictEqual(actions[0].actionType, "click");
  assert.strictEqual(actions[0].locatorStrategy, "ROLE");
}

function testFillAndSelectOptionMapping() {
  withTempFiles((tempDir) => {
    const codeFile = path.join(tempDir, "Code.ts");
    const outputFile = path.join(tempDir, "Code.cs");
    writeAppSettings(tempDir);
    fs.writeFileSync(codeFile, `
await page.getByLabel('Username').fill('admin');
await page.locator('#ctl00_ContentPlaceHolder1_ddlSearchUser').selectOption('T10964');
`, "utf8");

    const result = generateCSharpCode({
      codeFilePath: codeFile,
      outputFilePath: outputFile,
      appSettingsPath: path.join(tempDir, "appsettings.json")
    });

    assert.ok(result.code.includes('await Page.GetByLabel("Username").FillAsync("admin");'));
    assert.ok(result.code.includes('await Page.Locator("#ctl00_ContentPlaceHolder1_ddlSearchUser").SelectOptionAsync(new[] { "T10964" });'));
    assert.strictEqual(result.validation.success, true);
  });
}

function testGotoUsesConfiguredUrl() {
  withTempFiles((tempDir) => {
    const codeFile = path.join(tempDir, "Code.ts");
    const outputFile = path.join(tempDir, "Code.cs");
    writeAppSettings(tempDir);
    fs.writeFileSync(codeFile, "await page.goto('https://documentadministration-sit.trimont.com/adminReassign.aspx');", "utf8");

    const result = generateCSharpCode({
      codeFilePath: codeFile,
      outputFilePath: outputFile,
      appSettingsPath: path.join(tempDir, "appsettings.json")
    });

    assert.ok(result.code.includes("await Page.GotoAsync(Config.Urls.DocAdmin);"));
  });
}

function testTableLocatorConversion() {
  const code = "await page.locator(\"//tr[@id='ctl00_cp1_gd_ctl00__0']/td[2]/a\").click();";
  const result = generateCSharpCode({ codeFilePath: "", outputFilePath: "", appSettingsPath: "" });
  void result;
  const actions = parseRecordedActions(code);
  assert.strictEqual(actions[0].locatorStrategy, "TABLE_LOCATOR");
}

function testFrameConversion() {
  withTempFiles((tempDir) => {
    const codeFile = path.join(tempDir, "Code.ts");
    const outputFile = path.join(tempDir, "Code.cs");
    writeAppSettings(tempDir);
    fs.writeFileSync(codeFile, "await page.locator('iframe[name=\"UpdateTransactionDetails\"]').contentFrame().getByText('891224', { exact: true }).click();", "utf8");

    const result = generateCSharpCode({
      codeFilePath: codeFile,
      outputFilePath: outputFile,
      appSettingsPath: path.join(tempDir, "appsettings.json")
    });

    assert.ok(result.code.includes('Page.FrameLocator("iframe[name=\\"UpdateTransactionDetails\\"]").GetByText("891224", new() { Exact = true }).ClickAsync();'));
  });
}

function testAssertionConversion() {
  withTempFiles((tempDir) => {
    const codeFile = path.join(tempDir, "Code.ts");
    const outputFile = path.join(tempDir, "Code.cs");
    writeAppSettings(tempDir);
    fs.writeFileSync(codeFile, "await expect(page.getByText('Saved')).toBeVisible();", "utf8");

    const result = generateCSharpCode({
      codeFilePath: codeFile,
      outputFilePath: outputFile,
      appSettingsPath: path.join(tempDir, "appsettings.json")
    });

    assert.ok(result.code.includes('await Assertions.Expect(Page.GetByText("Saved")).ToBeVisibleAsync();'));
  });
}

function testUnsupportedActionIsReported() {
  withTempFiles((tempDir) => {
    const codeFile = path.join(tempDir, "Code.ts");
    const outputFile = path.join(tempDir, "Code.cs");
    writeAppSettings(tempDir);
    fs.writeFileSync(codeFile, "await page.route('**/*', () => {});", "utf8");

    const result = generateCSharpCode({
      codeFilePath: codeFile,
      outputFilePath: outputFile,
      appSettingsPath: path.join(tempDir, "appsettings.json")
    });

    assert.strictEqual(result.unsupportedActions.length, 1);
    assert.ok(result.code.includes("// Unsupported:"));
  });
}

function testGeneratedCodeValidation() {
  const validation = validateGeneratedCSharp(`
using Microsoft.Playwright;
public class Sample {
    public IPage Page { get; }
    public async Task ReplayAsync() {
        await Page.ClickAsync();
    }
}
`, []);

  assert.strictEqual(validation.success, true);
}

function runAllTests() {
  testClickMapping();
  testFillAndSelectOptionMapping();
  testGotoUsesConfiguredUrl();
  testTableLocatorConversion();
  testFrameConversion();
  testAssertionConversion();
  testUnsupportedActionIsReported();
  testGeneratedCodeValidation();
  console.log("CSharpCodeGenerator tests passed");
}

if (require.main === module) {
  runAllTests();
}

module.exports = {
  runAllTests
};