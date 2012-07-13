$base_dir = Split-Path $MyInvocation.MyCommand.Path

.\Packages\psake.4.2.0.1\tools\psake.ps1 .\Tools\build.ps1 -framework "4.0x86" -parameters @{"base_dir"="$base_dir"}