FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Frontend/Jp.UI.SSO/Jp.UI.SSO.csproj", "Frontend/Jp.UI.SSO/"]
COPY ["Backend/Jp.Infra.Data/Jp.Infra.Data.csproj", "Backend/Jp.Infra.Data/"]
COPY ["Backend/Jp.Domain.Core/Jp.Domain.Core.csproj", "Backend/Jp.Domain.Core/"]
COPY ["Backend/Jp.Domain/Jp.Domain.csproj", "Backend/Jp.Domain/"]
COPY ["Backend/Jp.Infra.CrossCutting.Bus/Jp.Infra.CrossCutting.Bus.csproj", "Backend/Jp.Infra.CrossCutting.Bus/"]
COPY ["Backend/Jp.Application/Jp.Application.csproj", "Backend/Jp.Application/"]
COPY ["Backend/Jp.Infra.CrossCutting.Identity/Jp.Infra.CrossCutting.Identity.csproj", "Backend/Jp.Infra.CrossCutting.Identity/"]
COPY ["Backend/Jp.Infra.CrossCutting.IdentityServer/Jp.Infra.CrossCutting.IdentityServer.csproj", "Backend/Jp.Infra.CrossCutting.IdentityServer/"]
COPY ["Backend/Jp.Infra.CrossCutting.IoC/Jp.Infra.CrossCutting.IoC.csproj", "Backend/Jp.Infra.CrossCutting.IoC/"]
COPY ["Backend/Jp.Infra.CrossCutting.Tools/Jp.Infra.CrossCutting.Tools.csproj", "Backend/Jp.Infra.CrossCutting.Tools/"]
RUN dotnet restore "Frontend/Jp.UI.SSO/Jp.UI.SSO.csproj"
COPY . .
WORKDIR "/src/Frontend/Jp.UI.SSO"
RUN dotnet build "Jp.UI.SSO.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Jp.UI.SSO.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Jp.UI.SSO.dll", "--server.urls", "http://*:5000"]