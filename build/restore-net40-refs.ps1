<#
.SYNOPSIS
    Downloads the .NET Framework 4.0 reference assemblies into the NuGet global package cache.

.DESCRIPTION
    Visual Studio no longer ships a .NET Framework 4.0 targeting pack, so a clean machine
    cannot build these v4.0 projects (MSBuild error MSB3644). Microsoft publishes the 4.0
    reference assemblies as the NuGet package Microsoft.NETFramework.ReferenceAssemblies.net40;
    Directory.Build.targets in the repo root imports that package's targets file.

    This script restores the package into the shared NuGet cache, so it only needs to be run
    once per machine (it serves both the Healthscope and CDAAU repositories).

    Requires the dotnet CLI on PATH.
#>
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

$packageId = 'Microsoft.NETFramework.ReferenceAssemblies.net40'
$version   = '1.0.3'

$packageRoot = if ($env:NUGET_PACKAGES) { $env:NUGET_PACKAGES } else { Join-Path $env:USERPROFILE '.nuget\packages' }
$installed   = Join-Path $packageRoot "$($packageId.ToLowerInvariant())\$version\build\.NETFramework\v4.0\mscorlib.dll"

if (Test-Path $installed) {
    Write-Host "$packageId $version is already present in $packageRoot" -ForegroundColor Green
    return
}

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw 'The dotnet CLI was not found on PATH. Install the .NET SDK and re-run this script.'
}

$work = Join-Path ([System.IO.Path]::GetTempPath()) ("net40refs-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $work -Force | Out-Null

try {
    @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="$packageId" Version="$version" />
  </ItemGroup>
</Project>
"@ | Set-Content -Path (Join-Path $work 'net40refs.csproj') -Encoding UTF8

    Write-Host "Restoring $packageId $version ..." -ForegroundColor Cyan
    & dotnet restore (Join-Path $work 'net40refs.csproj') --verbosity quiet
    if ($LASTEXITCODE -ne 0) { throw "dotnet restore failed with exit code $LASTEXITCODE." }
}
finally {
    Remove-Item $work -Recurse -Force -ErrorAction SilentlyContinue
}

if (-not (Test-Path $installed)) {
    throw "Restore completed but the reference assemblies were not found at $installed."
}

Write-Host "Done. .NET Framework 4.0 reference assemblies are available in $packageRoot" -ForegroundColor Green
