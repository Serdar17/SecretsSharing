FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SecretsSharing.WebApi/SecretsSharing.WebApi.csproj", "SecretsSharing.WebApi/"]
COPY ["SecretsSharing.Persistence/SecretsSharing.Persistence.csproj", "SecretsSharing.Persistence/"]
COPY ["SecretsSharing.Domain/SecretsSharing.Domain.csproj", "SecretsSharing.Domain/"]
RUN dotnet restore "SecretsSharing.WebApi/SecretsSharing.WebApi.csproj"
COPY . .
WORKDIR "/src/SecretsSharing.WebApi"
RUN dotnet build "SecretsSharing.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecretsSharing.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretsSharing.WebApi.dll", "--environment=Development"]
