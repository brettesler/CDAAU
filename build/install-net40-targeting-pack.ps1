<#
.SYNOPSIS
    Installs the .NET Framework 4.0 targeting pack so Visual Studio will load v4.0 projects.

.DESCRIPTION
    Directory.Build.targets is enough for MSBuild and command-line builds, but Visual Studio
    decides whether a target framework is "supported" at project-load time by looking for an
    installed targeting pack at

        %ProgramFiles(x86)%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0

    That folder ships with Visual Studio but contains only IntelliSense redirect stubs: no
    reference assemblies and no RedistList\FrameworkList.xml. Without those, VS shows the
    "Target framework not supported" dialog and offers to retarget the project to 4.8.

    This script copies the reference assemblies and the redist list out of the
    Microsoft.NETFramework.ReferenceAssemblies.net40 NuGet package into that folder, which is
    what the original targeting pack installer did.

    Only the .dll files and RedistList are copied. The existing *.xml files are deliberately
    left alone: they are redirect stubs pointing at the v4.X folder where the real IntelliSense
    documentation lives, so overwriting them would add ~114 MB for no benefit.

    Requires elevation; the script re-launches itself elevated if needed.

.NOTES
    Machine-wide, so it only needs running once. It serves both the Healthscope and CDAAU repos.
#>
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

$isElevated = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
    [Security.Principal.WindowsBuiltInRole]::Administrator)

if (-not $isElevated) {
    Write-Host 'Elevation is required to write to Program Files. Re-launching...' -ForegroundColor Yellow
    $psExe = (Get-Process -Id $PID).Path
    Start-Process -FilePath $psExe -Verb RunAs -ArgumentList @(
        '-NoProfile', '-ExecutionPolicy', 'Bypass', '-NoExit', '-File', "`"$PSCommandPath`"")
    return
}

$version     = '1.0.3'
$packageRoot = if ($env:NUGET_PACKAGES) { $env:NUGET_PACKAGES } else { Join-Path $env:USERPROFILE '.nuget\packages' }
$source      = Join-Path $packageRoot "microsoft.netframework.referenceassemblies.net40\$version\build\.NETFramework\v4.0"

if (-not (Test-Path (Join-Path $source 'mscorlib.dll'))) {
    Write-Host 'Reference assemblies not found in the NuGet cache; restoring them first...' -ForegroundColor Cyan
    & (Join-Path $PSScriptRoot 'restore-net40-refs.ps1')
}
if (-not (Test-Path (Join-Path $source 'mscorlib.dll'))) {
    throw "Could not locate the reference assemblies at $source."
}

$target = Join-Path ${env:ProgramFiles(x86)} 'Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0'
New-Item -ItemType Directory -Force -Path $target | Out-Null
New-Item -ItemType Directory -Force -Path (Join-Path $target 'RedistList') | Out-Null

Write-Host "Installing .NET Framework 4.0 targeting pack into:" -ForegroundColor Cyan
Write-Host "  $target"

# Copy every file except the IntelliSense documentation, which already exists as redirect
# stubs, but keep the RedistList files: FrameworkList.xml is what marks the pack installed.
# This mirrors the whole tree, so the Client Profile under Profile\Client is installed too.
$files = Get-ChildItem -Path $source -Recurse -File |
    Where-Object { $_.Extension -ne '.xml' -or $_.Directory.Name -eq 'RedistList' }

foreach ($file in $files) {
    $relative = $file.FullName.Substring($source.Length).TrimStart('\')
    $destination = Join-Path $target $relative
    $destinationDir = Split-Path $destination -Parent
    if (-not (Test-Path $destinationDir)) { New-Item -ItemType Directory -Force -Path $destinationDir | Out-Null }
    Copy-Item -Path $file.FullName -Destination $destination -Force
}

$installedDlls = (Get-ChildItem (Join-Path $target '*.dll')).Count
$clientDlls    = (Get-ChildItem (Join-Path $target 'Profile\Client\*.dll') -ErrorAction SilentlyContinue).Count
$hasList       = Test-Path (Join-Path $target 'RedistList\FrameworkList.xml')

Write-Host ''
Write-Host "Copied $($files.Count) files." -ForegroundColor Green
Write-Host "  Full framework reference assemblies: $installedDlls" -ForegroundColor Green
Write-Host "  Client Profile reference assemblies: $clientDlls" -ForegroundColor Green
Write-Host "  RedistList\FrameworkList.xml present: $hasList" -ForegroundColor Green
Write-Host 'Close Visual Studio completely and reopen the solution for the change to take effect.' -ForegroundColor Yellow
