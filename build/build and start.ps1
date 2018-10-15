$currentPath = (Get-Item -Path "./" -Verbose).FullName;
$src = (Get-Item -Path "../src" -Verbose).FullName;
$sso = (Get-Item -Path ($src + "/Frontend/Jp.UI.SSO") -Verbose).FullName
$api = (Get-Item -Path ($src + "/Backend/Jp.UserManagement") -Verbose).FullName
$ui = (Get-Item -Path ($src + "/Frontend/Jp.UserManagement") -Verbose).FullName
$adminui = (Get-Item -Path ($src + "/Frontend/Jp.AdminUI") -Verbose).FullName

$paramsSSO= "/c cd " + $sso + " && dotnet run";
$paramsAPI= "/c cd " + $api + " && dotnet run";
$paramsUI= "/c cd " + $ui + " && npm start";
$paramsAdminUI= "/c cd " + $adminui + " && npm start";

Write-Information Restore deps
dotnet build $sso
dotnet build $api
# The best way to npm install is from src folder
cd $ui; npm install
cd $currentPath
cd $adminui; npm install
cd $currentPath


# Start-Process -Verb runas "cmd.exe" $paramsSSO;
Start-Process "cmd.exe" $paramsAPI;
Start-Process "cmd.exe" $paramsSSO;
Start-Process "cmd.exe" $paramsUI;
Start-Process "cmd.exe" $paramsAdminUI;