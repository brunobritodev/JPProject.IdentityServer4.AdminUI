
echo Restore nuget dep's
dotnet build "../src/Backend/Jp.UserManagement"
dotnet build "../src/Frontend/Jp.UI.SSO"

CLS
echo Build UserManagement
start /d "../src/Frontend/Jp.UserManagement" npm install 

echo Build AdminUi
start /d "../src/Frontend/Jp.AdminUI" npm install

CLS
ECHO WAIT BUILD TO COMPLETE, THEN Run start.bat
PAUSE