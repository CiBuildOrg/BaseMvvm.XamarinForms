$path = "src\BaseMvvm\Properties\AssemblyInfo.cs" 
$pattern = '\[assembly: AssemblyVersion\("(\d\.\d\.\d\.\d)"\)\]'
$content = Get-Content $path;
$rslt = $content -match $pattern;
$SlnVersion = $rslt -replace '[assembly: AssemblyVersion\(")]' ,"";
$SlnVersion = $SlnVersion -replace '\[' ,"";
$SlnVersion = $SlnVersion -replace '\]' ,"";
Write-Host "BaseMvvm.XamarinForms Version:" $SlnVersion;
.\nuget pack BaseMvvm.XamarinForms.nuspec -version $SlnVersion;