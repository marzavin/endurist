FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Endurist.Worker/Endurist.Worker.csproj", "src/Endurist.Worker/"]
RUN dotnet restore "./src/Endurist.Worker/Endurist.Worker.csproj"
COPY . .
WORKDIR "/src/src/Endurist.Worker"
RUN dotnet build "./Endurist.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Endurist.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Endurist.Worker.dll"]