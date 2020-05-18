#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shop.ProductService.Api/Shop.ProductService.Api.csproj", "Shop.ProductService.Api/"]
COPY ["Shop.Domain/Shop.Domain.csproj", "Shop.Domain/"]
COPY ["Shop.Core/Shop.Core.csproj", "Shop.Core/"]
RUN dotnet restore "Shop.ProductService.Api/Shop.ProductService.Api.csproj"
COPY . .
WORKDIR "/src/Shop.ProductService.Api"
RUN dotnet build "Shop.ProductService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.ProductService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.ProductService.Api.dll"]
