#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shop.BasketService.Api/Shop.BasketService.Api.csproj", "Shop.BasketService.Api/"]
COPY ["Shop.Core/Shop.Core.csproj", "Shop.Core/"]
COPY ["Shop.Domain/Shop.Domain.csproj", "Shop.Domain/"]
RUN dotnet restore "Shop.BasketService.Api/Shop.BasketService.Api.csproj"
COPY . .
WORKDIR "/src/Shop.BasketService.Api"
RUN dotnet build "Shop.BasketService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.BasketService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.BasketService.Api.dll"]
