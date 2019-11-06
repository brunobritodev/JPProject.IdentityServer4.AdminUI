$currentPath = (Get-Item -Path "./" -Verbose).FullName;
$src = (Get-Item -Path "../src" -Verbose).FullName;
$api = (Get-Item -Path ($src + "/Backend/JPProject.Admin.Api") -Verbose).FullName
$adminui = (Get-Item -Path ($src + "/Frontend/Jp.AdminUI") -Verbose).FullName

$paramsAPI= "/c cd " + $api + " && dotnet run";
$paramsAdminUI= "/c cd " + $adminui + " && npm start";

Write-Information Restore deps
dotnet build $api
# The best way to npm install is from src folder
cd $adminui; npm install
cd $currentPath


# Start-Process -Verb runas "cmd.exe" $paramsSSO;
Start-Process "cmd.exe" $paramsAPI;
Start-Process "cmd.exe" $paramsAdminUI;