FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stringer.Data/Stringer.Data.csproj", "Stringer.Data/"]
RUN dotnet restore "Stringer.Data/Stringer.Data.csproj"
COPY . .
WORKDIR "/src/Stringer.Data"
RUN dotnet build "Stringer.Data.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stringer.Data.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stringer.Data.dll"]
