$framework = '4.0x86'

properties {
	$build_dir 	 	 = "Build"
	$release_dir 	 = "Release"
	$sln_file 	 	 = "Source\EmbeddedMelodyPlayer.sln"
	$nuget_base_file = "Source\packages\repositories.config"
}

function Get-NugetRepositoriesPaths() {
	$nugetRepositories = ([xml](Get-Content $nuget_base_file)).repositories.repository	
	$nugetRepositoriesPaths = @()
	foreach ($nugetRepository in $nugetRepositories) {
		$completeRepositoryPath = (Split-Path($nuget_base_file)) + "\" + $nugetRepository.path
		$nugetRepositoriesPaths += @($completeRepositoryPath)
	}
	return $nugetRepositoriesPaths
}

function Download-Dependencies($nugetRepositoriesPaths) {
	$dependenciesDestinationDir = Split-Path($nuget_base_file)	
	foreach ($nugetRepositoryPath in $nugetRepositoriesPaths) {
		exec { Tools\NuGet\NuGet install $nugetRepositoryPath -o $dependenciesDestinationDir }
	}
}

function Get-TestAssemblies() {
	$projects = Get-AllProjects
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

function Get-AllProjects() {
	$projects = @()
	Get-Content $sln_file | 
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

function Run-Tests($testAssemblies) {
	foreach ($testAssembly in $testAssemblies) {
		write "---------------------------------------------"
		write ("Running tests for assembly " + $testAssembly.Name + ".")
		write "---------------------------------------------"
		
		$assemblyPath = $testAssembly.DirectoryName + "\" + $testAssembly.Name		
		exec { .\Tools\nunit\nunit-console.exe $assemblyPath /nologo }
	}
}

task default -depends RunTests

task Clean {
	Remove-Item $build_dir -Recurse -ErrorAction SilentlyContinue
	Remove-Item $release_dir -Recurse -ErrorAction SilentlyContinue
}

task Init -depends Clean {
	New-Item -Type Directory $build_dir > $null
	New-Item -Type Directory $release_dir > $null
}

task DownloadDependencies -depends Init {
	$nugetRepositoriesPaths = Get-NugetRepositoriesPaths
	Download-Dependencies($nugetRepositoriesPaths)
}

task Compile -depends DownloadDependencies {
	exec { MSBuild $sln_file /property:Configuration=Release /verbosity:quiet /nologo }
}

task RunTests -depends Compile {
	$testAssemblies = Get-TestAssemblies
	Run-Tests($testAssemblies)
}