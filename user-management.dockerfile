FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5003/tcp
ENV ASPNETCORE_URLS http://*:5003

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/Backend/Jp.UserManagement/Jp.UserManagement.csproj", "Backend/Jp.UserManagement/"]
COPY ["src/Backend/Jp.Domain.Core/Jp.Domain.Core.csproj", "Backend/Jp.Domain.Core/"]
COPY ["src/Backend/Jp.Infra.Data/Jp.Infra.Data.csproj", "Backend/Jp.Infra.Data/"]
COPY ["src/Backend/Jp.Domain/Jp.Domain.csproj", "Backend/Jp.Domain/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Bus/Jp.Infra.CrossCutting.Bus.csproj", "Backend/Jp.Infra.CrossCutting.Bus/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Identity/Jp.Infra.CrossCutting.Identity.csproj", "Backend/Jp.Infra.CrossCutting.Identity/"]
COPY ["src/Backend/Jp.Application/Jp.Application.csproj", "Backend/Jp.Application/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.IoC/Jp.Infra.CrossCutting.IoC.csproj", "Backend/Jp.Infra.CrossCutting.IoC/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Tools/Jp.Infra.CrossCutting.Tools.csproj", "Backend/Jp.Infra.CrossCutting.Tools/"]
RUN dotnet restore "Backend/Jp.UserManagement/Jp.UserManagement.csproj"
COPY src/ .
WORKDIR "/src/Backend/Jp.UserManagement"
RUN dotnet build "Jp.UserManagement.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Jp.UserManagement.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Jp.UserManagement.dll", "--server.urls", "http://*:5003"]