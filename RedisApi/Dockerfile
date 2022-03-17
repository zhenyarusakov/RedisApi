FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedisApi/RedisApi.csproj", "RedisApi/"]
RUN dotnet restore "RedisApi/RedisApi.csproj"
COPY . .
WORKDIR "/src/RedisApi"
RUN dotnet build "RedisApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisApi.dll"]
