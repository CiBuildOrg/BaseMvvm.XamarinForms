if($($env:APPVEYOR))
{
    $SVersion = $($env:APPVEYOR_BUILD_VERSION);
    Write-Host $SVersion
\.SlnVersionChange $SVersion;
    Write-Host "BaseMvvm.XamarinForms Version:" $SVersion;
.\nuget pack BaseMvvm.XamarinForms.nuspec -version $SVersion;
}else{
    Write-Host "AppVeyor was not detected"
}