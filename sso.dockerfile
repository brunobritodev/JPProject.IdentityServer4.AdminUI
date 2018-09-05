FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/Frontend/Jp.UI.SSO/Jp.UI.SSO.csproj", "Frontend/Jp.UI.SSO/"]
COPY ["src/Backend/Jp.Infra.Data/Jp.Infra.Data.csproj", "Backend/Jp.Infra.Data/"]
COPY ["src/Backend/Jp.Domain.Core/Jp.Domain.Core.csproj", "Backend/Jp.Domain.Core/"]
COPY ["src/Backend/Jp.Domain/Jp.Domain.csproj", "Backend/Jp.Domain/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Bus/Jp.Infra.CrossCutting.Bus.csproj", "Backend/Jp.Infra.CrossCutting.Bus/"]
COPY ["src/Backend/Jp.Application/Jp.Application.csproj", "Backend/Jp.Application/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Identity/Jp.Infra.CrossCutting.Identity.csproj", "Backend/Jp.Infra.CrossCutting.Identity/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.IdentityServer/Jp.Infra.CrossCutting.IdentityServer.csproj", "Backend/Jp.Infra.CrossCutting.IdentityServer/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.IoC/Jp.Infra.CrossCutting.IoC.csproj", "Backend/Jp.Infra.CrossCutting.IoC/"]
COPY ["src/Backend/Jp.Infra.CrossCutting.Tools/Jp.Infra.CrossCutting.Tools.csproj", "Backend/Jp.Infra.CrossCutting.Tools/"]
COPY ["keys/JPProjectOAuth.cer", "/usr/local/share/ca-certificates"]
RUN dotnet restore "Frontend/Jp.UI.SSO/Jp.UI.SSO.csproj"
COPY src/ .
WORKDIR "/src/Frontend/Jp.UI.SSO"
RUN dotnet build "Jp.UI.SSO.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Jp.UI.SSO.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY ["src/Frontend/Jp.UI.SSO/JPProjectOAuth.pfx", "."]
ENTRYPOINT ["dotnet", "Jp.UI.SSO.dll", "--server.urls", "http://*:5000"]