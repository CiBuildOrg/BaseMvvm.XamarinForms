if($($env:APPVEYOR))
{
	.\nuget restore BaseMvvm.XamarinForms.sln
	$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\amd64\MSBuild.exe"
	& $msbuild BaseMvvm.XamarinForms.sln /property:Configuration=Release /target:Clean /target:Build
	.\CreateNugetPackage.ps1
}else{
    Write-Host "AppVeyor was not detected"
}