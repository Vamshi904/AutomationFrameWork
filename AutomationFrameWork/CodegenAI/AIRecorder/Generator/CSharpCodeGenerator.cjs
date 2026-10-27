const fs = require("fs");
const path = require("path");

function safeRead(filePath) {
  if (!filePath || !fs.existsSync(filePath)) {
    return "";
  }

  return fs.readFileSync(filePath, "utf8");
}

function parseArgs(argv) {
  const args = {};
  for (let index = 2; index < argv.length; index += 1) {
    const token = argv[index];
    if (!token.startsWith("--")) {
      continue;
    }

    const [key, inlineValue] = token.split("=");
    const name = key.slice(2);
    if (inlineValue !== undefined) {
      args[name] = inlineValue;
      continue;
    }

    const next = argv[index + 1];
    if (next && !next.startsWith("--")) {
      args[name] = next;
      index += 1;
    } else {
      args[name] = "true";
    }
  }

  return args;
}

function escapeCSharpString(value) {
  return String(value || "")
    .replace(/\\/g, "\\\\")
    .replace(/"/g, '\\"');
}

function toPascalCase(value) {
  return String(value || "")
    .split(/[^a-zA-Z0-9]+/)
    .filter(Boolean)
    .map((segment) => segment.charAt(0).toUpperCase() + segment.slice(1).toLowerCase())
    .join("");
}

function inferRoleEnum(role) {
  const normalized = String(role || "").trim().toLowerCase();
  const explicitMap = {
    button: "Button",
    link: "Link",
    cell: "Cell",
    row: "Row",
    textbox: "Textbox",
    combobox: "Combobox",
    checkbox: "Checkbox",
    radio: "Radio",
    dialog: "Dialog",
    table: "Table",
    grid: "Grid",
    option: "Option",
    tab: "Tab",
    tabpanel: "TabPanel",
    heading: "Heading",
    img: "Img",
    image: "Img"
  };

  return explicitMap[normalized] || toPascalCase(normalized);
}

function readConfiguredUrls(appSettingsPath) {
  try {
    const parsed = JSON.parse(safeRead(appSettingsPath) || "{}");
    const urls = parsed && parsed.Urls ? parsed.Urls : {};
    return Object.entries(urls)
      .filter((entry) => typeof entry[1] === "string" && entry[1].trim().length > 0)
      .map(([key, value]) => ({ key, value }));
  } catch {
    return [];
  }
}

function buildUrlExpression(url, appSettingsPath) {
  const configuredUrls = readConfiguredUrls(appSettingsPath);
  const matched = configuredUrls.find((entry) => entry.value === url);

  if (matched) {
    return {
      expression: `Config.Urls.${matched.key}`,
      warning: null
    };
  }

  return {
    expression: `"${escapeCSharpString(url)}"`,
    warning: `URL at source line uses literal value because no matching appsettings Urls entry was found: ${url}`
  };
}

function detectTableInfo(selector) {
  const text = String(selector || "");
  const probable = /\/\/tr\[|role='row'|role='cell'|grid|table|rg[a-z]+/i.test(text);
  return probable
    ? {
        probable: true,
        selector
      }
    : null;
}

function createAction(sourceLine, raw, data) {
  return {
    sourceLine,
    raw,
    actionType: data.actionType,
    locator: data.locator || null,
    locatorStrategy: data.locatorStrategy || "UNKNOWN",
    targetElement: data.targetElement || null,
    value: data.value === undefined ? null : data.value,
    expectedResult: data.expectedResult || null,
    frame: data.frame || null,
    popup: data.popup || null,
    table: data.table || null,
    confidence: data.confidence === undefined ? 0.7 : data.confidence,
    supported: data.supported !== false,
    diagnostics: Array.isArray(data.diagnostics) ? data.diagnostics : []
  };
}

function parseActionLine(rawLine, sourceLine) {
  const line = String(rawLine || "").trim();
  if (!line.startsWith("await ")) {
    return null;
  }

  const gotoMatch = line.match(/^await\s+page\.goto\((['"`])([^'"`]+)\1\);$/);
  if (gotoMatch) {
    return createAction(sourceLine, rawLine, {
      actionType: "goto",
      locatorStrategy: "PAGE_URL",
      value: gotoMatch[2],
      targetElement: "page"
    });
  }

  const waitForSelectorMatch = line.match(/^await\s+page\.waitForSelector\((['"`])(.+?)\1\);$/);
  if (waitForSelectorMatch) {
    return createAction(sourceLine, rawLine, {
      actionType: "waitForSelector",
      locatorStrategy: "SELECTOR",
      locator: { kind: "selector", selector: waitForSelectorMatch[2] },
      targetElement: "selector"
    });
  }

  const keyboardMatch = line.match(/^await\s+page\.keyboard\.(press|type|insertText|down|up)\((['"`])([^'"`]*)\2\);$/);
  if (keyboardMatch) {
    return createAction(sourceLine, rawLine, {
      actionType: `keyboard.${keyboardMatch[1]}`,
      locatorStrategy: "KEYBOARD",
      value: keyboardMatch[3],
      targetElement: "keyboard"
    });
  }

  const mouseMatch = line.match(/^await\s+page\.mouse\.(click|dblclick|move)\(([^\)]*)\);$/);
  if (mouseMatch) {
    const values = mouseMatch[2]
      .split(",")
      .map((part) => part.trim())
      .filter(Boolean);

    return createAction(sourceLine, rawLine, {
      actionType: `mouse.${mouseMatch[1]}`,
      locatorStrategy: "MOUSE",
      value: values,
      targetElement: "mouse"
    });
  }

  const expectVisibleMatch = line.match(/^await\s+expect\((.+)\)\.(toBeVisible|toContainText|toHaveText)\((?:\s*(['"`])([\s\S]*?)\3\s*)?\);$/);
  if (expectVisibleMatch) {
    return createAction(sourceLine, rawLine, {
      actionType: expectVisibleMatch[2],
      locatorStrategy: "ASSERTION",
      locator: { kind: "expression", expression: expectVisibleMatch[1].trim() },
      expectedResult: expectVisibleMatch[4] || expectVisibleMatch[2],
      targetElement: "assertion"
    });
  }

  const patterns = [
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)\.contentFrame\(\)\.getByRole\((['"`])(\w+)\3,\s*\{\s*name:\s*(['"`])([^'"`]+)\5(?:,\s*exact:\s*(true|false))?\s*\}\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[8],
        locatorStrategy: "FRAME_ROLE",
        locator: { kind: "frameRole", frameSelector: matches[2], role: matches[4], name: matches[6], exact: matches[7] === "true" },
        targetElement: matches[4],
        frame: { selector: matches[2] },
        confidence: 0.9
      })
    },
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)\.contentFrame\(\)\.getByText\((['"`])([^'"`]+)\3(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[6],
        locatorStrategy: "FRAME_TEXT",
        locator: { kind: "frameText", frameSelector: matches[2], text: matches[4], exact: matches[5] === "true" },
        targetElement: "text",
        frame: { selector: matches[2] },
        confidence: 0.88
      })
    },
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)\.getByRole\((['"`])(\w+)\3,\s*\{\s*name:\s*(['"`])([^'"`]+)\5(?:,\s*exact:\s*(true|false))?\s*\}\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[8],
        locatorStrategy: "SCOPED_ROLE",
        locator: { kind: "scopedRole", selector: matches[2], role: matches[4], name: matches[6], exact: matches[7] === "true" },
        targetElement: matches[4],
        table: detectTableInfo(matches[2]),
        confidence: 0.92
      })
    },
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)\.getByText\((['"`])([^'"`]+)\3(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[6],
        locatorStrategy: "SCOPED_TEXT",
        locator: { kind: "scopedText", selector: matches[2], text: matches[4], exact: matches[5] === "true" },
        targetElement: "text",
        table: detectTableInfo(matches[2]),
        confidence: 0.9
      })
    },
    {
      match: line.match(/^await\s+page\.getByRole\((['"`])(\w+)\1,\s*\{\s*name:\s*(['"`])([^'"`]+)\3(?:,\s*exact:\s*(true|false))?\s*\}\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[6],
        locatorStrategy: "ROLE",
        locator: { kind: "role", role: matches[2], name: matches[4], exact: matches[5] === "true" },
        targetElement: matches[2],
        confidence: 0.95
      })
    },
    {
      match: line.match(/^await\s+page\.getByText\((['"`])([^'"`]+)\1(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)\.(click|dblclick|hover|check|uncheck|focus)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[4],
        locatorStrategy: "TEXT",
        locator: { kind: "text", text: matches[2], exact: matches[3] === "true" },
        targetElement: "text",
        confidence: 0.82
      })
    },
    {
      match: line.match(/^await\s+page\.getByLabel\((['"`])([^'"`]+)\1(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)\.(fill|press|focus|click)\((?:\s*(['"`])([\s\S]*?)\5\s*)?\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[4],
        locatorStrategy: "LABEL",
        locator: { kind: "label", text: matches[2], exact: matches[3] === "true" },
        targetElement: "label",
        value: matches[6] || null,
        confidence: 0.92,
        diagnostics: matches[4] === "fill" && matches[6] ? ["Literal input value preserved from recording. Consider replacing with framework data binding."] : []
      })
    },
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)(\.first\(\))?\.(click|dblclick|hover|check|uncheck|focus|waitFor)\(\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[4],
        locatorStrategy: detectTableInfo(matches[2]) ? "TABLE_LOCATOR" : "LOCATOR",
        locator: { kind: "locator", selector: matches[2], first: Boolean(matches[3]) },
        targetElement: "locator",
        table: detectTableInfo(matches[2]),
        confidence: 0.93
      })
    },
    {
      match: line.match(/^await\s+page\.locator\((['"`])(.+?)\1\)\.(fill|press|selectOption|setInputFiles)\((['"`])([\s\S]*?)\4\);$/),
      build: (matches) => createAction(sourceLine, rawLine, {
        actionType: matches[3],
        locatorStrategy: detectTableInfo(matches[2]) ? "TABLE_LOCATOR" : "LOCATOR",
        locator: { kind: "locator", selector: matches[2], first: false },
        targetElement: "locator",
        value: matches[5],
        table: detectTableInfo(matches[2]),
        confidence: 0.9,
        diagnostics: ["Literal input value preserved from recording. Consider replacing with framework data binding."]
      })
    }
  ];

  for (const pattern of patterns) {
    if (pattern.match) {
      return pattern.build(pattern.match);
    }
  }

  return createAction(sourceLine, rawLine, {
    actionType: "unsupported",
    locatorStrategy: "UNSUPPORTED",
    supported: false,
    confidence: 0,
    diagnostics: ["Unsupported Playwright statement. Manual review required."]
  });
}

function parseRecordedActions(code) {
  const lines = String(code || "").split(/\r?\n/);
  const actions = [];

  for (let index = 0; index < lines.length; index += 1) {
    const action = parseActionLine(lines[index], index + 1);
    if (action) {
      actions.push(action);
    }
  }

  return actions;
}

function convertLocatorExpressionFromTs(expression) {
  const trimmed = String(expression || "").trim();

  const roleMatch = trimmed.match(/^page\.getByRole\((['"`])(\w+)\1,\s*\{\s*name:\s*(['"`])([^'"`]+)\3(?:,\s*exact:\s*(true|false))?\s*\}\)$/);
  if (roleMatch) {
    const exactPart = roleMatch[5] === "true" ? ", Exact = true" : "";
    return `Page.GetByRole(AriaRole.${inferRoleEnum(roleMatch[2])}, new() { Name = \"${escapeCSharpString(roleMatch[4])}\"${exactPart} })`;
  }

  const textMatch = trimmed.match(/^page\.getByText\((['"`])([^'"`]+)\1(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)$/);
  if (textMatch) {
    const options = textMatch[3] === "true" ? ", new() { Exact = true }" : "";
    return `Page.GetByText(\"${escapeCSharpString(textMatch[2])}\"${options})`;
  }

  const labelMatch = trimmed.match(/^page\.getByLabel\((['"`])([^'"`]+)\1(?:,\s*\{\s*exact:\s*(true|false)\s*\})?\)$/);
  if (labelMatch) {
    const options = labelMatch[3] === "true" ? ", new() { Exact = true }" : "";
    return `Page.GetByLabel(\"${escapeCSharpString(labelMatch[2])}\"${options})`;
  }

  const locatorMatch = trimmed.match(/^page\.locator\((['"`])(.+?)\1\)$/);
  if (locatorMatch) {
    return `Page.Locator(\"${escapeCSharpString(locatorMatch[2])}\")`;
  }

  return trimmed
    .replace(/page\./g, "Page.")
    .replace(/getByRole\(/g, "GetByRole(")
    .replace(/getByText\(/g, "GetByText(")
    .replace(/getByLabel\(/g, "GetByLabel(")
    .replace(/locator\(/g, "Locator(")
    .replace(/'/g, '"');
}

function buildLocatorExpression(locator) {
  if (!locator) {
    return "Page";
  }

  if (locator.kind === "role") {
    const exactPart = locator.exact ? ", Exact = true" : "";
    return `Page.GetByRole(AriaRole.${inferRoleEnum(locator.role)}, new() { Name = \"${escapeCSharpString(locator.name)}\"${exactPart} })`;
  }

  if (locator.kind === "text") {
    const options = locator.exact ? ", new() { Exact = true }" : "";
    return `Page.GetByText(\"${escapeCSharpString(locator.text)}\"${options})`;
  }

  if (locator.kind === "label") {
    const options = locator.exact ? ", new() { Exact = true }" : "";
    return `Page.GetByLabel(\"${escapeCSharpString(locator.text)}\"${options})`;
  }

  if (locator.kind === "locator") {
    const base = `Page.Locator(\"${escapeCSharpString(locator.selector)}\")`;
    return locator.first ? `${base}.First` : base;
  }

  if (locator.kind === "scopedRole") {
    const exactPart = locator.exact ? ", Exact = true" : "";
    return `Page.Locator(\"${escapeCSharpString(locator.selector)}\").GetByRole(AriaRole.${inferRoleEnum(locator.role)}, new() { Name = \"${escapeCSharpString(locator.name)}\"${exactPart} })`;
  }

  if (locator.kind === "scopedText") {
    const options = locator.exact ? ", new() { Exact = true }" : "";
    return `Page.Locator(\"${escapeCSharpString(locator.selector)}\").GetByText(\"${escapeCSharpString(locator.text)}\"${options})`;
  }

  if (locator.kind === "frameRole") {
    const exactPart = locator.exact ? ", Exact = true" : "";
    return `Page.FrameLocator(\"${escapeCSharpString(locator.frameSelector)}\").GetByRole(AriaRole.${inferRoleEnum(locator.role)}, new() { Name = \"${escapeCSharpString(locator.name)}\"${exactPart} })`;
  }

  if (locator.kind === "frameText") {
    const options = locator.exact ? ", new() { Exact = true }" : "";
    return `Page.FrameLocator(\"${escapeCSharpString(locator.frameSelector)}\").GetByText(\"${escapeCSharpString(locator.text)}\"${options})`;
  }

  if (locator.kind === "expression") {
    return convertLocatorExpressionFromTs(locator.expression);
  }

  return "Page";
}

function buildActionStatement(action, options) {
  const prefix = `            // Source: Code.ts line ${action.sourceLine}`;

  if (!action.supported) {
    return [prefix, `            // Unsupported: ${action.raw.trim()}`];
  }

  if (action.actionType === "goto") {
    const resolved = buildUrlExpression(action.value, options.appSettingsPath);
    const lines = [prefix];
    if (resolved.warning) {
      lines.push(`            // Review: ${resolved.warning}`);
    }
    lines.push(`            await Page.GotoAsync(${resolved.expression});`);
    return lines;
  }

  if (action.actionType === "waitForSelector") {
    return [
      prefix,
      `            await Page.WaitForSelectorAsync(\"${escapeCSharpString(action.locator.selector)}\");`
    ];
  }

  if (action.actionType.startsWith("keyboard.")) {
    const methodName = action.actionType.split(".")[1];
    const mapping = {
      press: "PressAsync",
      type: "TypeAsync",
      insertText: "InsertTextAsync",
      down: "DownAsync",
      up: "UpAsync"
    };
    return [
      prefix,
      `            await Page.Keyboard.${mapping[methodName]}(\"${escapeCSharpString(action.value)}\");`
    ];
  }

  if (action.actionType.startsWith("mouse.")) {
    const methodName = action.actionType.split(".")[1];
    const values = Array.isArray(action.value) ? action.value : [];
    const x = values[0] || "0";
    const y = values[1] || "0";
    if (methodName === "move") {
      return [prefix, `            await Page.Mouse.MoveAsync(${x}, ${y});`];
    }

    const mapped = methodName === "dblclick" ? "DblClickAsync" : "ClickAsync";
    return [prefix, `            await Page.Mouse.${mapped}(${x}, ${y});`];
  }

  if (["toBeVisible", "toHaveText", "toContainText"].includes(action.actionType)) {
    const locatorExpression = buildLocatorExpression(action.locator);
    if (action.actionType === "toBeVisible") {
      return [prefix, `            await Assertions.Expect(${locatorExpression}).ToBeVisibleAsync();`];
    }

    if (action.actionType === "toHaveText") {
      return [prefix, `            await Assertions.Expect(${locatorExpression}).ToHaveTextAsync(\"${escapeCSharpString(action.expectedResult)}\");`];
    }

    return [prefix, `            await Assertions.Expect(${locatorExpression}).ToContainTextAsync(\"${escapeCSharpString(action.expectedResult)}\");`];
  }

  const locatorExpression = buildLocatorExpression(action.locator);
  const warnings = action.diagnostics.map((message) => `            // Review: ${message}`);
  const methodMap = {
    click: "ClickAsync",
    dblclick: "DblClickAsync",
    hover: "HoverAsync",
    check: "CheckAsync",
    uncheck: "UncheckAsync",
    focus: "FocusAsync"
  };

  if (methodMap[action.actionType]) {
    return [prefix, ...warnings, `            await ${locatorExpression}.${methodMap[action.actionType]}();`];
  }

  if (action.actionType === "fill") {
    return [prefix, ...warnings, `            await ${locatorExpression}.FillAsync(\"${escapeCSharpString(action.value)}\");`];
  }

  if (action.actionType === "press") {
    return [prefix, ...warnings, `            await ${locatorExpression}.PressAsync(\"${escapeCSharpString(action.value)}\");`];
  }

  if (action.actionType === "selectOption") {
    return [prefix, ...warnings, `            await ${locatorExpression}.SelectOptionAsync(new[] { \"${escapeCSharpString(action.value)}\" });`];
  }

  if (action.actionType === "setInputFiles") {
    return [prefix, ...warnings, `            await ${locatorExpression}.SetInputFilesAsync(\"${escapeCSharpString(action.value)}\");`];
  }

  if (action.actionType === "waitFor") {
    return [prefix, `            await ${locatorExpression}.WaitForAsync();`];
  }

  return [prefix, `            // Unsupported mapping: ${action.raw.trim()}`];
}

function buildCode(actions, options) {
  const bodyLines = [];
  for (const action of actions) {
    bodyLines.push(...buildActionStatement(action, options), "");
  }

  return [
    "using System;",
    "using System.Threading.Tasks;",
    "using AutomationFrameWork.Utilities;",
    "using Microsoft.Playwright;",
    "",
    "namespace AutomationFrameWork.CodegenAI.AIRecorder",
    "{",
    "    public sealed class CodegenRecordedFlow",
    "    {",
    "        public CodegenRecordedFlow(IPage page, ConfigReader config)",
    "        {",
    "            Page = page ?? throw new ArgumentNullException(nameof(page));",
    "            Config = config ?? throw new ArgumentNullException(nameof(config));",
    "        }",
    "",
    "        public IPage Page { get; }",
    "",
    "        public ConfigReader Config { get; }",
    "",
    "        public async Task ReplayAsync()",
    "        {",
    ...bodyLines,
    "        }",
    "    }",
    "}"
  ].join("\n");
}

function validateGeneratedCSharp(code, actions) {
  const errors = [];
  const warnings = [];

  if (!code.includes("using Microsoft.Playwright;")) {
    errors.push("Missing Microsoft.Playwright using.");
  }

  if (!code.includes("public async Task ReplayAsync()")) {
    errors.push("ReplayAsync method was not generated.");
  }

  if ((code.match(/\{/g) || []).length !== (code.match(/\}/g) || []).length) {
    errors.push("Generated code has unbalanced braces.");
  }

  if (code.includes("page.")) {
    errors.push("Generated code still contains lowercase TypeScript page references.");
  }

  if (code.includes("TODO_UNRESOLVED")) {
    errors.push("Generated code contains unresolved placeholders.");
  }

  const unsupported = actions.filter((action) => !action.supported);
  if (unsupported.length > 0) {
    warnings.push(`${unsupported.length} recorded action(s) require manual review.`);
  }

  return {
    success: errors.length === 0,
    errors,
    warnings
  };
}

function generateCSharpCode(options) {
  const code = safeRead(options.codeFilePath);
  const actions = parseRecordedActions(code);
  const builtCode = buildCode(actions, options);
  const validation = validateGeneratedCSharp(builtCode, actions);
  const unsupportedActions = actions.filter((action) => !action.supported);
  const result = {
    generatedAtUtc: new Date().toISOString(),
    codeFilePath: options.codeFilePath,
    outputFilePath: options.outputFilePath,
    actions,
    unsupportedActions,
    validation,
    code: builtCode
  };

  if (options.outputFilePath) {
    fs.mkdirSync(path.dirname(options.outputFilePath), { recursive: true });
    fs.writeFileSync(options.outputFilePath, builtCode, "utf8");
  }

  if (options.reportFilePath) {
    fs.mkdirSync(path.dirname(options.reportFilePath), { recursive: true });
    fs.writeFileSync(options.reportFilePath, JSON.stringify({
      generatedAtUtc: result.generatedAtUtc,
      codeFilePath: result.codeFilePath,
      outputFilePath: result.outputFilePath,
      actionCount: result.actions.length,
      unsupportedActions: result.unsupportedActions.map((action) => ({
        sourceLine: action.sourceLine,
        raw: action.raw,
        diagnostics: action.diagnostics
      })),
      validation: result.validation
    }, null, 2), "utf8");
  }

  return result;
}

function runCli() {
  const args = parseArgs(process.argv);
  const codeFilePath = args["code-file"];
  const outputFilePath = args["output-file"];

  if (!codeFilePath || !outputFilePath) {
    console.error("[CSharpCodeGenerator] Missing required arguments --code-file and --output-file");
    process.exit(2);
  }

  const projectRoot = path.resolve(path.dirname(codeFilePath), "..", "..");
  const result = generateCSharpCode({
    codeFilePath,
    outputFilePath,
    reportFilePath: args["report-file"] || "",
    appSettingsPath: args["appsettings-file"] || path.join(projectRoot, "appsettings.json")
  });

  console.log(`[CSharpCodeGenerator] Generated ${path.basename(outputFilePath)} with ${result.actions.length} action(s). unsupported=${result.unsupportedActions.length}`);
  if (!result.validation.success) {
    console.error(`[CSharpCodeGenerator] Validation failed: ${result.validation.errors.join(" | ")}`);
    process.exit(1);
  }
}

if (require.main === module) {
  runCli();
}

module.exports = {
  buildCode,
  generateCSharpCode,
  parseRecordedActions,
  validateGeneratedCSharp
};