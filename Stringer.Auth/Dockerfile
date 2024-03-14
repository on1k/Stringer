FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stringer.Auth/Stringer.Auth.csproj", "Stringer.Auth/"]
RUN dotnet restore "Stringer.Auth/Stringer.Auth.csproj"
COPY . .
WORKDIR "/src/Stringer.Auth"
RUN dotnet build "Stringer.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stringer.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stringer.Auth.dll"]
