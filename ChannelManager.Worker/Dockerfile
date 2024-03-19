FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChannelManager.Worker/ChannelManager.Worker.csproj", "ChannelManager.Worker/"]
COPY ["ChannelManager.Domain/ChannelManager.Domain.csproj", "ChannelManager.Domain/"]
COPY ["ChannelManager.Data/ChannelManager.Data.csproj", "ChannelManager.Data/"]
RUN dotnet restore "./ChannelManager.Worker/./ChannelManager.Worker.csproj"
COPY . .
WORKDIR "/src/ChannelManager.Worker"
RUN dotnet build "./ChannelManager.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChannelManager.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChannelManager.Worker.dll"]