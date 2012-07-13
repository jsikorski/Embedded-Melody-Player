function Download-Dependencies {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$nugetPath,
		
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$nugetBaseFile
	)

	$nugetRepositoriesPaths = Get-NugetRepositoriesPaths -nugetBaseFile $nugetBaseFile
	
	$dependenciesDestinationDir = Split-Path($nugetBaseFile)	
	foreach ($nugetRepositoryPath in $nugetRepositoriesPaths) {
		exec { & $nugetPath install $nugetRepositoryPath -o $dependenciesDestinationDir }
	}
}

function Get-NugetRepositoriesPaths() {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$nugetBaseFile
	)
	
	$nugetRepositories = ([xml](Get-Content $nugetBaseFile)).repositories.repository	
	$nugetRepositoriesPaths = @()
	foreach ($nugetRepository in $nugetRepositories) {
		$completeRepositoryPath = (Split-Path($nugetBaseFile)) + "\" + $nugetRepository.path
		$nugetRepositoriesPaths += @($completeRepositoryPath)
	}
	return $nugetRepositoriesPaths
}

function Run-Tests {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$slnFile,
	
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$nunitPath
	)

	$projects = Get-AllProjects -slnFile $slnFile
	$testAssemblies = Get-TestAssemblies -projects $projects
	
	foreach ($testAssembly in $testAssemblies) {
		write "---------------------------------------------"
		write ("Running tests for assembly " + $testAssembly.Name + ".")
		write "---------------------------------------------"
		
		$assemblyPath = $testAssembly.DirectoryName + "\" + $testAssembly.Name		
		exec { & $nunitPath $assemblyPath /nologo }
	}
}

function Get-AllProjects() {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[string]
		$slnFile
	)
	
	$projects = @()
	Get-Content $slnFile | 
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

function Get-TestAssemblies() {
	param(
		[Parameter(Mandatory=$true)]
		[ValidateNotNullOrEmpty()]
		[System.Object[]]
		$projects
	)

	$testAssemblies = @()
	foreach ($project in $projects) {
		Get-ChildItem $build_dir -Recurse -Filter (($project.Name + ".dll")) | 
		ForEach-Object {			
			if (Test-Path ($_.DirectoryName + "\nunit.framework.dll")) {
				$testAssembliesPaths += $_
			}
		}
	}
	return $testAssembliesPaths	
}

