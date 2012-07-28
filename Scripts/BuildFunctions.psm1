function Get-Dependencies {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$NugetPath,
		
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$NugetBaseFile
	)

	$nugetRepositoriesPaths = Get-NugetRepositoriesPaths -NugetBaseFile $NugetBaseFile
	
	$dependenciesDestinationDir = Split-Path($NugetBaseFile)
	foreach ($nugetRepositoryPath in $nugetRepositoriesPaths) {
		exec { & $NugetPath install $nugetRepositoryPath -o $dependenciesDestinationDir }
	}
}

function Get-NugetRepositoriesPaths() {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$NugetBaseFile
	)
	
	$nugetRepositories = ([xml](Get-Content $NugetBaseFile)).repositories.repository	
	$nugetRepositoriesPaths = @()
	foreach ($nugetRepository in $nugetRepositories) {
		$completeRepositoryPath = (Split-Path($NugetBaseFile)) + "\" + $nugetRepository.path
		$nugetRepositoriesPaths += @($completeRepositoryPath)
	}
	return $nugetRepositoriesPaths
}

function Start-Tests {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$SlnFile,
	
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$NUnitPath, 
		
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$BuildDir,
		
		[string]
		$TestsResultsFile = "TestsResults.xml"
	)

	$projects = Get-AllProjects -SlnFile $SlnFile
	$testAssemblies = Get-TestAssemblies -Projects $Projects -BuildDir $BuildDir
	
	foreach ($testAssembly in $testAssemblies) {
		write "---------------------------------------------"
		write ("Running tests for assembly " + $testAssembly.Name + ".")
		write "---------------------------------------------"
		
		$assemblyPath = $testAssembly.DirectoryName + "\" + $testAssembly.Name		
		exec { & $NUnitPath $assemblyPath /nologo /result=$TestsResultsFile }
	}
}

function Get-AllProjects {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$SlnFile
	)
	
	$projects = @()
	Get-Content $SlnFile | 
		Select-String "Project\(" | 
			ForEach-Object {
				$projectParts = $_ -Split '[,=]' | ForEach-Object { $_.Trim('[ "{}]') };
				$projects += @(New-Object PSObject -Property @{
					Name = $projectParts[1]
					File = $projectParts[2]
					Guid = $projectParts[3]
				})
			}
	return $projects
}

function Get-TestAssemblies {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[System.Object[]]
		$Projects, 
		
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$BuildDir
	)

	$testAssembliesPaths = @()
	foreach ($project in $Projects) {
		Get-ChildItem $BuildDir -Recurse -Filter (($project.Name + ".dll")) | 
		ForEach-Object {
			if (Test-Path ($_.DirectoryName + "\nunit.framework.dll")) {
				$testAssembliesPaths += $_
			}
		}
	}
	
	return $testAssembliesPaths	
}

