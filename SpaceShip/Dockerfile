# Set the base image to build the app using the SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution and projects files
COPY *.sln .
COPY ./src/SpaceShip.WebAPI/*.csproj ./src/SpaceShip.WebAPI/
COPY ./src/SpaceShip.Service/*.csproj ./src/SpaceShip.Service/
COPY ./src/SpaceShip.Service.EventsProvider/*.csproj ./src/SpaceShip.Service.EventsProvider/
COPY ./mock/MockSpaceShip.Service/*.csproj ./mock/MockSpaceShip.Service/

# Restore packages
RUN dotnet restore

# Copy the rest of the source files
COPY . ./

# Add default developer certificate
RUN dotnet dev-certs https

# Build and publish the app in Release mode
RUN dotnet publish -c Release -o out ./src/SpaceShip.WebAPI/WebAPI.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

# Expose port for the API to run on
EXPOSE 7141
EXPOSE 5216

ENTRYPOINT ["dotnet", "WebAPI.dll"]
