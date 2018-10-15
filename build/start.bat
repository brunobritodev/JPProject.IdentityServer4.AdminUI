
CLS
echo RUN API
start /d "../src/Backend/Jp.UserManagement" dotnet run args

echo RUN SSO
start /d "../src/Frontend/Jp.UI.SSO" dotnet run args

echo RUN UserManagement
start /d "../src/Frontend/Jp.UserManagement" npm start

echo RUN AdminUi
start /d "../src/Frontend/Jp.AdminUI" npm start

