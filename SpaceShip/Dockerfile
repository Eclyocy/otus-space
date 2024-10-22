# Set the base image to build the app using the SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Copy Shared projects
WORKDIR /app/Shared
COPY ./Shared/*.csproj .

# Copy solution and projects files
WORKDIR /app/SpaceShip
COPY ./SpaceShip/*.sln .
COPY ./SpaceShip/src/SpaceShip.Domain/*.csproj ./src/SpaceShip.Domain/
COPY ./SpaceShip/src/SpaceShip.WebAPI/*.csproj ./src/SpaceShip.WebAPI/
COPY ./SpaceShip/src/SpaceShip.Notifications/*.csproj ./src/SpaceShip.Notifications/
COPY ./SpaceShip/src/SpaceShip.Service/*.csproj ./src/SpaceShip.Service/
COPY ./SpaceShip/src/SpaceShip.Service.EventsProvider/*.csproj ./src/SpaceShip.Service.EventsProvider/
COPY ./SpaceShip/test/SpaceShip.UnitTest/*.csproj ./test/SpaceShip.UnitTest/

# Restore packages
RUN dotnet restore

# Copy the rest of the source files
WORKDIR /app
COPY . ./

# Add default developer certificate
RUN dotnet dev-certs https

# Build and publish the app in Release mode
WORKDIR /app/SpaceShip
RUN dotnet publish -c Release -o out ./src/SpaceShip.WebAPI/WebAPI.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app/SpaseShip
COPY --from=build-env /app/SpaceShip/out .
COPY --from=build-env /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

# Get curl for healthcheck
RUN apk add --no-cache curl

# Expose port for the API to run on
EXPOSE 7141
EXPOSE 5216

ENTRYPOINT ["dotnet", "WebAPI.dll"]
