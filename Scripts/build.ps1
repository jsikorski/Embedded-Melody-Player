Import-Module .\BuildFunctions.psm1

properties {
	$buildDir 	  	  = "$baseDir\Build"
	$releaseDir    	  = "$baseDir\Release"
	$slnFile 	   	  = "$baseDir\Source\EmbeddedMelodyPlayer.sln"
	$nugetBaseFile	  = "$baseDir\Packages\repositories.config"
	$toolsDir 	   	  = "$baseDir\Tools"
	$nugetPath 	   	  = "$toolsDir\NuGet\NuGet.exe"
	$nUnitPath 	   	  = "$baseDir\Tools\nunit\nunit-console.exe"
	$resultsDir	   	  = "$buildDir\Results"
	$testsResultsFile = "$resultsDir\TestsResults.xml"
}

task default -depends Compile, RunTests

task Clean {
	Remove-Item $buildDir -Recurse -ErrorAction SilentlyContinue
	Remove-Item $releaseDir -Recurse -ErrorAction SilentlyContinue
}

task Init -depends Clean {
	New-Item -Type Directory $buildDir > $null
	New-Item -Type Directory $resultsDir > $null
	New-Item -Type Directory $releaseDir > $null
}

task DownloadDependencies {
	Get-Dependencies -NugetPath $nugetPath -NugetBaseFile $nugetBaseFile
}

task Compile -depends Init, DownloadDependencies {
	exec { MSBuild $slnFile /property:Configuration=Release /verbosity:quiet /nologo }
}

task RunTests -depends Compile {
	Start-Tests -SlnFile $slnFile -NUnitPath $nUnitPath -BuildDir $buildDir -TestsResultsFile $testsResultsFile
}