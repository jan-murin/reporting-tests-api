param(
	[string]$DatabaseManagerDir,
	[string]$DatabaseConfigDir,
	[string]$ApplicationName,
	[string]$ConnectionString
)

Write-Host "DatabaseManagerDir:" $DatabaseManagerDir
Write-Host "DatabaseConfigDir:" $DatabaseConfigDir
Write-Host "ApplicationName:" $ApplicationName
Write-Host "ConnectionString:" $ConnectionString

if ($ConnectionString -eq $null) {
    Write-Error "Connection string not specified"
    return
}

Write-Host "Recreating database for API tests..."

Push-Location $DatabaseManagerDir # changing location to DatabaseManager folder
& ".\Flow.DatabaseManager.DotNetCore.exe" --Drop --UpdateAll --Application $ApplicationName --ConnectionString $ConnectionString --DatabaseConfigDir $DatabaseConfigDir
Pop-Location # restoring original location

if ($lastexitcode -ne 0)
{
	Write-Error "Error in database update scripts"
} else {
    Write-Host "Recreated"
}