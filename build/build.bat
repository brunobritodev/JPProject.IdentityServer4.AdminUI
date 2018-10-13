
echo Restore nuget dep's
dotnet build "../src/Backend/Jp.UserManagement"
dotnet build "../src/Frontend/Jp.UI.SSO"

CLS
echo RUN API
start /d "../src/Backend/Jp.UserManagement" dotnet run args

echo RUN SSO
start /d "../src/Frontend/Jp.UI.SSO" dotnet run args

echo RUN UserManagement
(cd "../src/Frontend/Jp.UserManagement" && npm install && start npm start)

echo RUN AdminUi
(cd "../src/Frontend/Jp.AdminUI" && npm install && start npm start)

