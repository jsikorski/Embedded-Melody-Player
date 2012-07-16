$base_dir = Split-Path $MyInvocation.MyCommand.Path

$nuget_sln_config = "$base_dir\Source\.nuget\packages.config"
& "$base_dir\Tools\NuGet\NuGet.exe" install $nuget_sln_config -OutputDir Packages

$psake = Get-ChildItem -Recurse -Include psake.ps1
& $psake .\Scripts\build.ps1 -framework "4.0x86" -parameters @{"base_dir"="$base_dir"}