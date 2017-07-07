if($($env:APPVEYOR))
{ 
	$SVersion = $($env:APPVEYOR_BUILD_VERSION);
    Write-Host "BaseMvvm.XamarinForms Version:$SVersion";
.\nuget pack BaseMvvm.XamarinForms.nuspec -version $SVersion

    Push-AppveyorArtifact "C:\projects\basemvvm-xamarinforms\BaseMvvm.XamarinForms.$SVersion.nupkg" -name "NugetPackage1";
}
else{
    Write-Host "AppVeyor was not detected";
}