. .\buildHelpers.ps1

properties {
	$build_dir 	 	 = "$base_dir\Build"
	$release_dir 	 = "$base_dir\Release"
	$sln_file 	 	 = "$base_dir\Source\EmbeddedMelodyPlayer.sln"
	$nuget_base_file = "$base_dir\Packages\repositories.config"
	$tools_dir 		 = "$base_dir\Tools"
	$nuget_path 	 = "$tools_dir\NuGet\NuGet.exe"
	$nunit_path 	 = "$base_dir\Tools\nunit\nunit-console.exe"
}

task default -depends Compile, RunTests

task Clean {
	Remove-Item $build_dir -Recurse -ErrorAction SilentlyContinue
	Remove-Item $release_dir -Recurse -ErrorAction SilentlyContinue
}

task Init -depends Clean {
	New-Item -Type Directory $build_dir > $null
	New-Item -Type Directory $release_dir > $null
}

task DownloadDependencies {
	Download-Dependencies -nugetPath $nuget_path -nugetBaseFile $nuget_base_file
}

task Compile -depends Init, DownloadDependencies {
	exec { MSBuild $sln_file /property:Configuration=Release /verbosity:quiet /nologo }
}

task RunTests -depends Compile {
	Run-Tests -slnFile $sln_file -nunitPath $nunit_path
}