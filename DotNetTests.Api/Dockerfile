#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotNetTests.Api/DotNetTests.Api.csproj", "DotNetTests.Api/"]
COPY ["DotNetTests.Application/DotNetTests.Application.csproj", "DotNetTests.Application/"]
COPY ["DotNetTests.EntityFrameworkCore/DotNetTests.EntityFrameworkCore.csproj", "DotNetTests.EntityFrameworkCore/"]
COPY ["DotNetTests.Infrastructure/DotNetTests.Infrastructure.csproj", "DotNetTests.Infrastructure/"]
COPY ["DotNetTests.Domain/DotNetTests.Domain.csproj", "DotNetTests.Domain/"]
RUN dotnet restore "DotNetTests.Api/DotNetTests.Api.csproj"
COPY . .
WORKDIR "/src/DotNetTests.Api"
RUN dotnet build "DotNetTests.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetTests.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetTests.Api.dll"]