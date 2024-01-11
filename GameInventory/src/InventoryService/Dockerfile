# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY GameInventory/src/InventoryService/*.csproj ./
COPY packages/ packages/
COPY GameCommon/src/GameCommon/Protos/ Protos/
RUN dotnet new nugetconfig
RUN dotnet nuget add source packages/
RUN dotnet restore

# copy everything else and build app
COPY GameInventory/src/InventoryService/ ./
RUN dotnet publish -c release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "InventoryService.dll"]