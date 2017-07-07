if($($env:APPVEYOR))
{ 
    Update-AppveyorBuild -Version "$(Get-Date -format 1.y.M.dhhmm)";
    $SVersion = $($env:APPVEYOR_BUILD_VERSION);
    Write-Host "BaseMvvm.XamarinForms Version:$SVersion";

.\nuget restore BaseMvvm.XamarinForms.sln;
    $msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\MSBuild.exe"
    & $msbuild BaseMvvm.XamarinForms.sln /property:Configuration=Release

cd C:\projects\basemvvm-xamarinforms\
    .\nuget pack BaseMvvm.XamarinForms.nuspec -version $SVersion;

    Push-AppveyorArtifact "C:\projects\basemvvm-xamarinforms\BaseMvvm.XamarinForms.$SVersion.nupkg" -FileName "BaseMvvm.XamarinForms.$SVersion.nupkg" -DeploymentName "NugetPackage1";
}
else
{
    Write-Host "AppVeyor was not detected";
}