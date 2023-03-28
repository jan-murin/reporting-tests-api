param(
	[string]$ApplicationName,
	[string]$Version
)

if (!$version) {
    $version = $OctopusParameters["Octopus.Release.Number"];
}

$applicationPath = "C:\Program Files (x86)\Visma Unique AS\Visma Flyt $ApplicationName API Tests"
$testsDll = "$applicationPath\$ApplicationName.Tests.API.dll"
$extentReportIndex = "$applicationPath\Reports\index.html"

Write-Host "Running DotNet command: dotnet vstest "$testsDll""
dotnet vstest "$testsDll"

#Exposing report file artifact in Octopus
if (Test-Path $extentReportIndex) {
	Write-Host "Extent report index found. Creating artifact from path: $extentReportIndex"
	New-OctopusArtifact -Path $extentReportIndex -Name "API-TEST-INDEX.html"
} else {
	Write-Warning "Extent report index not found. Path: $extentReportIndex"
}

# report to Octopus if API passed successfully or not
if ($LASTEXITCODE -eq 0)
{
	Write-Host "API tests passed"
} else {
	Write-Error "API tests failed"
	Exit 1
}