# Set the base image to build the app using the SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution and projects files
COPY *.sln .
COPY EventGenerator/*.csproj ./EventGenerator/
COPY EventGenerator.Database/*.csproj ./EventGenerator.Database/
COPY EventGenerator.Services/*.csproj ./EventGenerator.Services/
COPY EventGenerator.Services.Tests/*.csproj ./EventGenerator.Services.Tests/

# Restore packages
RUN dotnet restore

# Copy the rest of the source files
COPY . ./

# Add default developer certificate
RUN dotnet dev-certs https

# Build and publish the app in Release mode
RUN dotnet publish -c Release -o out EventGenerator/EventGenerator.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

# Get curl for healthcheck
RUN apt-get update && apt-get install -y curl && apt-get clean

# Expose port for the API to run on
EXPOSE 7161

ENTRYPOINT ["dotnet", "EventGenerator.API.dll"]
