#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["URLShortener.API/URLShortener.API.csproj", "URLShortener.API/"]
COPY ["URLShortener.Application/URLShortener.Application.csproj", "URLShortener.Application/"]
COPY ["URLShortener.Domain/URLShortener.Domain.csproj", "URLShortener.Domain/"]
COPY ["URLShortener.Resources/URLShortener.Resources.csproj", "URLShortener.Resources/"]
COPY ["URLShortener.Infrastructure/URLShortener.Infrastructure.csproj", "URLShortener.Infrastructure/"]
RUN dotnet restore "./URLShortener.API/URLShortener.API.csproj"
COPY . .
WORKDIR "/src/URLShortener.API"
RUN dotnet build "./URLShortener.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./URLShortener.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "URLShortener.API.dll"]