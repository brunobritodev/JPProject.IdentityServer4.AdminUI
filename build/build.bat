
echo Restore nuget dep's
dotnet build "../src/Backend/JPProject.Admin.Api"

echo Build AdminUi
start /d "../src/Frontend/Jp.AdminUI" npm install

CLS
ECHO WAIT BUILD TO COMPLETE, THEN Run start.bat
PAUSE