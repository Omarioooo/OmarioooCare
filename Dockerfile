# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE ${HTTP_PORT}
EXPOSE ${HTTPS_PORT}

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Hospital System/OmarioooCare.csproj", "Hospital System/"]
RUN dotnet restore "Hospital System/OmarioooCare.csproj"
COPY . .
WORKDIR "/src/Hospital System"
RUN dotnet build "OmarioooCare.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "OmarioooCare.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmarioooCare.dll"]
