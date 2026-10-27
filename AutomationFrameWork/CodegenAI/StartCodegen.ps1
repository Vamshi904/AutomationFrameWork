param(
	[string]$TargetUrl = 'https://scrubdb-uat.trimont.com/
	',
	[string]$TestIdAttribute = 'data-testid',
	[string]$TableSelector = 'table',
	[switch]$SkipLiveObservation,
	[switch]$TableLocatorDebug,
	[switch]$SkipTableLocatorPostProcess,
	[switch]$SkipCSharpGeneration
)

Write-Host "Starting Playwright Codegen..."

$sessionScript = Join-Path $PSScriptRoot "AIRecorder\auth\SessionManager.ps1"
$postProcessScript = Join-Path $PSScriptRoot "AIRecorder\auth\PostProcessCodegen.ps1"
$liveObserverScript = Join-Path $PSScriptRoot "AIRecorder\Intelligence\LiveObserver.cjs"
$liveTableEngineScript = Join-Path $PSScriptRoot "AIRecorder\Intelligence\LiveTableLocatorEngine.cjs"
$csharpGeneratorScript = Join-Path $PSScriptRoot "AIRecorder\Generator\CSharpCodeGenerator.cjs"
$sessionPath = Join-Path $PSScriptRoot "AIRecorder\auth\session.json"
$outputFile = Join-Path $PSScriptRoot 'Code.ts'
$domObservationFile = Join-Path $PSScriptRoot "AIRecorder\LiveObservations.json"
$tableLocatorMetadataFile = Join-Path $PSScriptRoot "AIRecorder\TableLocatorMetadata.json"
$csharpOutputFile = Join-Path $PSScriptRoot "AIRecorder\Code.cs"
$csharpReportFile = Join-Path $PSScriptRoot "AIRecorder\CSharpGenerationReport.json"
$appSettingsFile = Join-Path (Split-Path $PSScriptRoot -Parent) "appsettings.json"
$maxAgeDays = 4

if (-not (Test-Path $sessionScript)) {
	throw "Session helper not found: $sessionScript"
}

. $sessionScript

Initialize-CodegenSession -SessionPath $sessionPath -MaxAgeDays $maxAgeDays
$loadExistingSession = Test-CodegenSessionValid -Path $sessionPath -MaxAgeDays $maxAgeDays

$codegenArgs = Get-CodegenArgs `
	-TargetUrl $TargetUrl `
	-OutputFile $outputFile `
	-SessionPath $sessionPath `
	-TestIdAttribute $TestIdAttribute `
	-LoadSession:$loadExistingSession

npx @codegenArgs

if ((-not $SkipLiveObservation) -or (-not $SkipTableLocatorPostProcess)) {
	if (Test-Path $liveTableEngineScript) {
		$engineArgs = @(
			$liveTableEngineScript,
			'--code-file', $outputFile,
			'--dom-file', $domObservationFile,
			'--metadata-file', $tableLocatorMetadataFile,
			'--storage-state-file', $sessionPath,
			'--table-selector', $TableSelector,
			'--skip-observation', $SkipLiveObservation.IsPresent.ToString().ToLower(),
			'--skip-rewrite', $SkipTableLocatorPostProcess.IsPresent.ToString().ToLower()
		)

		if ($TableLocatorDebug) {
			$engineArgs += @('--debug', 'true')
		}

		& node @engineArgs
		if ($LASTEXITCODE -ne 0) {
			Write-Warning "Live Table Locator Engine failed. Existing fallback behavior preserved."
		}
	} else {
		if (-not $SkipLiveObservation) {
			if (Test-Path $liveObserverScript) {
				$observerArgs = @(
					$liveObserverScript,
					'--code-file', $outputFile,
					'--output-file', $domObservationFile,
					'--storage-state-file', $sessionPath
				)

				if ($TableLocatorDebug) {
					$observerArgs += @('--debug', 'true')
				}

				& node @observerArgs
				if ($LASTEXITCODE -ne 0) {
					Write-Warning "Live observation failed. Continuing with fallback behavior."
				}
			} else {
				Write-Warning "Live observer not found: $liveObserverScript"
			}
		}

		if (-not $SkipTableLocatorPostProcess) {
			if (Test-Path $postProcessScript) {
				. $postProcessScript
				Invoke-CodegenTableLocatorPostProcess `
					-CodeFilePath $outputFile `
					-DomFilePath $domObservationFile `
					-MetadataFilePath $tableLocatorMetadataFile `
					-TableSelector $TableSelector `
					-Debug:$TableLocatorDebug
			} else {
				Write-Warning "Post processor not found: $postProcessScript"
			}
		}
	}
}

if (-not $SkipCSharpGeneration) {
	if (Test-Path $csharpGeneratorScript) {
		$csharpArgs = @(
			$csharpGeneratorScript,
			'--code-file', $outputFile,
			'--output-file', $csharpOutputFile,
			'--report-file', $csharpReportFile,
			'--appsettings-file', $appSettingsFile
		)

		& node @csharpArgs
		if ($LASTEXITCODE -ne 0) {
			Write-Warning "C# generation failed. Raw Code.ts and existing post-processing outputs were preserved."
		}
	} else {
		Write-Warning "C# generator not found: $csharpGeneratorScript"
	}
}

Write-Host "Codegen closed."