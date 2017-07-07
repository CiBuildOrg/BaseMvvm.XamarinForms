if($($env:APPVEYOR))
{ 
	$SVersion = $($env:APPVEYOR_BUILD_VERSION);
    Write-Host "BaseMvvm.XamarinForms Version:" $SVersion;
.\nuget pack BaseMvvm.XamarinForms.nuspec -version $SVersion;
	Push-AppveyorArtifact "BaseMvvm.XamarinForms." $SVersion ".nupkg";
}
else{
    Write-Host "AppVeyor was not detected"
}