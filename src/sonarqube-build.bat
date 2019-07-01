dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet build-server shutdown
dotnet sonarscanner begin /d:sonar.login=4fb6d7d978373797169fd04634f9892897cdb3fe /k:"jpproject" /d:sonar.cs.opencover.reportsPaths="**\coverage.opencover.xml"
dotnet build
dotnet sonarscanner end /d:sonar.login=4fb6d7d978373797169fd04634f9892897cdb3fe