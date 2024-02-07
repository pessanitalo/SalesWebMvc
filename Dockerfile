FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SalesWebMvc.App/SalesWebMvc.App.csproj", "SalesWebMvc.App/"]
COPY ["SalesWebMvc.Data/SalesWebMvc.Data.csproj", "SalesWebMvc.Data/"]
COPY ["SalesWebMvc.Business/SalesWebMvc.Business.csproj", "SalesWebMvc.Business/"]
RUN dotnet restore "./SalesWebMvc.App/./SalesWebMvc.App.csproj"
COPY . .
WORKDIR "/src/SalesWebMvc.App"
RUN dotnet build "./SalesWebMvc.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SalesWebMvc.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesWebMvc.App.dll"]