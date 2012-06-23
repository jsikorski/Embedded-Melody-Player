# Helper script for those who want to run
# psake without importing the module.
$scriptLocation = Split-Path($myInvocation.MyCommand.Path)

import-module $scriptLocation\psake.psm1
invoke-psake @args
remove-module psake