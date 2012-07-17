$baseDir = Split-Path $MyInvocation.MyCommand.Path

$nugetSlnConfig = "$baseDir\Source\.nuget\packages.config"
& "$baseDir\Tools\NuGet\NuGet.exe" install $nugetSlnConfig -OutputDir Packages

$psake = Get-ChildItem -Recurse -Include psake.ps1
& $psake .\Scripts\Build.ps1 -framework "4.0x86" -parameters @{"baseDir"=$baseDir}