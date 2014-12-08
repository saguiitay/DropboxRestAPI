properties { 
  $zipFileName = "DropboxRestAPI.zip"
  $majorVersion = "1.0"
  $majorWithReleaseVersion = "1.0.0"
  $version = GetVersion $majorWithReleaseVersion
  $buildDocumentation = $false
  $buildNuGet = $true
  $treatWarningsAsErrors = $false
  
  $baseDir  = resolve-path ..
  $buildDir = "$baseDir\Build"
  $sourceDir = "$baseDir\Src"
  $toolsDir = "$baseDir\Tools"
  $docDir = "$baseDir\Doc"
  $releaseDir = "$baseDir\Release"
  $workingDir = "$baseDir\Working"
  $builds = @(
    @{Name = "DropboxRestAPI"; Constants=""; FinalDir="Net45"; NuGetDir = "net45"; Framework="net-4.0"; },
    @{Name = "DropboxRestAPI.Portable"; Constants="PORTABLE"; FinalDir="Portable"; NuGetDir = "portable-net45+win8+wpa81"; Framework="net-4.0"; }
  )
}

$framework = '4.0x86'

task default -depends Package

# Ensure a clean working directory
task Clean {
  Set-Location $baseDir
  
  if (Test-Path -path $workingDir)
  {
    Write-Output "Deleting Working Directory"
    
    del $workingDir -Recurse -Force
  }
  
  New-Item -Path $workingDir -ItemType Directory
}

# Build each solution, optionally signed
task Build -depends Clean { 
  Write-Host -ForegroundColor Green "Updating assembly version"
  Write-Host
  Update-AssemblyInfoFiles $sourceDir ($majorVersion + '.0.0') $version

  foreach ($build in $builds)
  {
    $name = $build.Name
    if ($name -ne $null)
    {
      $finalDir = $build.FinalDir

      Write-Host -ForegroundColor Green "Building " $name

      Write-Host
      Write-Host "Restoring"
      exec { .\Tools\NuGet\NuGet.exe restore ".\Src\$name.sln" | Out-Default } "Error restoring $name"

      Write-Host
      Write-Host "Building"
      exec { msbuild "/t:Clean;Rebuild" /p:Configuration=Release "/p:Platform=Any CPU" /p:OutputPath=bin\Release\$finalDir\  "/p:TreatWarningsAsErrors=$treatWarningsAsErrors" "/p:VisualStudioVersion=12.0" (GetConstants $build.Constants) ".\Src\$name.sln" | Out-Default } "Error building $name"
    }
  }
}

# Optional build documentation, add files to final zip
task Package -depends Build {
  if ($buildNuGet)
  {
    New-Item -Path $workingDir\NuGet -ItemType Directory    
    Copy-Item -Path "$buildDir\DropboxRestAPI.nuspec" -Destination $workingDir\NuGet\DropboxRestAPI.nuspec -recurse

    foreach ($build in $builds)
    {
      if ($build.NuGetDir)
      {
        $name = $build.TestsName
        $finalDir = $build.FinalDir
        $frameworkDirs = $build.NuGetDir.Split(",")
        
        foreach ($frameworkDir in $frameworkDirs)
        {
          robocopy "$sourceDir\DropboxRestAPI\bin\Release\$finalDir" $workingDir\NuGet\lib\$frameworkDir *.dll *.pdb *.xml /NP /XO /XF *.CodeAnalysisLog.xml | Out-Default
        }
      }
    }
  
    exec { .\Tools\NuGet\NuGet.exe pack $workingDir\NuGet\DropboxRestAPI.nuspec -Symbols }
    move -Path .\*.nupkg -Destination $workingDir\NuGet
  }
  
  if ($buildDocumentation)
  {
    $mainBuild = $builds | where { $_.Name -eq "DropboxRestAPI" } | select -first 1
    $mainBuildFinalDir = $mainBuild.FinalDir
    $documentationSourcePath = "$workingDir\Package\Bin\$mainBuildFinalDir"
    Write-Host -ForegroundColor Green "Building documentation from $documentationSourcePath"

    # Sandcastle has issues when compiling with .NET 4 MSBuild - http://shfb.codeplex.com/Thread/View.aspx?ThreadId=50652
    exec { msbuild "/t:Clean;Rebuild" /p:Configuration=Release "/p:DocumentationSourcePath=$documentationSourcePath" $docDir\doc.shfbproj | Out-Default } "Error building documentation. Check that you have Sandcastle, Sandcastle Help File Builder and HTML Help Workshop installed."
    
    move -Path $workingDir\Documentation\LastBuild.log -Destination $workingDir\Documentation.log
  }
  
  # exclude package directories but keep packages\repositories.config
  $packageDirs = gci $sourceDir\packages | where {$_.PsIsContainer} | Select -ExpandProperty Name
}

function GetConstants($constants)
{
  return "/p:DefineConstants=`"CODE_ANALYSIS;TRACE;$constants`""
}

function GetVersion($majorVersion)
{
    $now = [DateTime]::Now
    
    $year = $now.Year - 2000
    $month = $now.Month
    $totalMonthsSince2000 = ($year * 12) + $month
    $day = $now.Day
    $minor = "{0}{1:00}" -f $totalMonthsSince2000, $day
    
    $hour = $now.Hour
    $minute = $now.Minute
    $revision = "{0:00}{1:00}" -f $hour, $minute
    
    return $majorVersion + "." + $minor
}

function Update-AssemblyInfoFiles ([string] $sourceDir, [string] $assemblyVersionNumber, [string] $fileVersionNumber)
{
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersion = 'AssemblyFileVersion("' + $fileVersionNumber + '")';
    
    Get-ChildItem -Path $sourceDir -r -filter AssemblyInfo.cs | ForEach-Object {
        
        $filename = $_.Directory.ToString() + '\' + $_.Name
        Write-Host $filename
        $filename + ' -> ' + $version
    
        (Get-Content $filename) | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
            % {$_ -replace $fileVersionPattern, $fileVersion }
        } | Set-Content $filename
    }
}
