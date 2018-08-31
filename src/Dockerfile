FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT Development
COPY ["Frontend/Equinox.UI.SSO/Equinox.UI.SSO.csproj", "Frontend/Equinox.UI.SSO/"]
COPY ["Backend/Equinox.Infra.CrossCutting.IoC/Equinox.Infra.CrossCutting.IoC.csproj", "Backend/Equinox.Infra.CrossCutting.IoC/"]
COPY ["Backend/Equinox.Domain/Equinox.Domain.csproj", "Backend/Equinox.Domain/"]
COPY ["Backend/Equinox.Infra.CrossCutting.Bus/Equinox.Infra.CrossCutting.Bus.csproj", "Backend/Equinox.Infra.CrossCutting.Bus/"]
COPY ["Backend/Equinox.Domain.Core/Equinox.Domain.Core.csproj", "Backend/Equinox.Domain.Core/"]
COPY ["Backend/Equinox.Infra.Data/Equinox.Infra.Data.csproj", "Backend/Equinox.Infra.Data/"]
COPY ["Backend/Equinox.Infra.CrossCutting.Tools/Equinox.Infra.CrossCutting.Tools.csproj", "Backend/Equinox.Infra.CrossCutting.Tools/"]
COPY ["Backend/Equinox.Application/Equinox.Application.csproj", "Backend/Equinox.Application/"]
COPY ["Backend/Equinox.Infra.CrossCutting.Identity/Equinox.Infra.CrossCutting.Identity.csproj", "Backend/Equinox.Infra.CrossCutting.Identity/"]
COPY ["Backend/Equinox.Infra.CrossCutting.IdentityServer/Equinox.Infra.CrossCutting.IdentityServer.csproj", "Backend/Equinox.Infra.CrossCutting.IdentityServer/"]
RUN dotnet restore "Frontend/Equinox.UI.SSO/Equinox.UI.SSO.csproj"
COPY . .
WORKDIR "/src/Frontend/Equinox.UI.SSO"
RUN dotnet build "Equinox.UI.SSO.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Equinox.UI.SSO.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Equinox.UI.SSO.dll"]