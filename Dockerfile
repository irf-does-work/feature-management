# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


# Copy all the project files to the container
COPY ["FeatureToggle.API/FeatureToggle.API.csproj", "FeatureToggle.API/"]
COPY ["FeatureToggle.Application/FeatureToggle.Application.csproj", "FeatureToggle.Application/"]
COPY ["FeatureToggle.Domain/FeatureToggle.Domain.csproj", "FeatureToggle.Domain/"]
COPY ["FeatureToggle.Infrastructure/FeatureToggle.Infrastructure.csproj", "FeatureToggle.Infrastructure/"]


# Restore dependencies
RUN dotnet restore "FeatureToggle.API/FeatureToggle.API.csproj"


# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "FeatureToggle.API/FeatureToggle.API.csproj" -c Release -o /app/build


# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FeatureToggle.API/FeatureToggle.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeatureToggle.API.dll"]