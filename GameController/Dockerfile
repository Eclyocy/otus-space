# Set the base image to build the app using the SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Copy Shared projects
WORKDIR /app/Shared
COPY ./Shared/*.csproj .

# Copy solution and projects files
WORKDIR /app/GameController
COPY ./GameController/*.sln .
COPY ./GameController/GameController.API/*.csproj ./GameController.API/
COPY ./GameController/GameController.Database/*.csproj ./GameController.Database/
COPY ./GameController/GameController.Services/*.csproj ./GameController.Services/
COPY ./GameController/GameController.Services.Tests/*.csproj ./GameController.Services.Tests/

# Restore packages
RUN dotnet restore

# Copy the rest of the source files
WORKDIR /app
COPY . ./

# Add default developer certificate
RUN dotnet dev-certs https

# Build and publish the app in Release mode
WORKDIR /app/GameController
RUN dotnet publish -c Release -o out GameController.API/GameController.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/GameController/out .
COPY --from=build-env /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

# Get curl for healthcheck
RUN apt-get update && apt-get install -y curl && apt-get clean

# Expose port for the API to run on
EXPOSE 7196

ENTRYPOINT ["dotnet", "GameController.API.dll"]
