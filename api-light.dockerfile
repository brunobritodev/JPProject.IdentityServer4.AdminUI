FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Backend/JPProject.Admin.Api/JPProject.Admin.Api.csproj", "Backend/JPProject.Admin.Api/"]
RUN dotnet restore "Backend/JPProject.Admin.Api/JPProject.Admin.Api.csproj"
COPY src/ .
WORKDIR "/src/Backend/JPProject.Admin.Api"
RUN dotnet build "JPProject.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JPProject.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JPProject.Admin.Api.dll"]