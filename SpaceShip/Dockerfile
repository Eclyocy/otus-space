FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5216

ENV ASPNETCORE_URLS=http://+:5216

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["SpaceShip/src/WebAPI/WebAPI.csproj", "SpaceShip/src/WebAPI/"]
RUN dotnet restore "SpaceShip/src/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/SpaceShip/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "WebAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]