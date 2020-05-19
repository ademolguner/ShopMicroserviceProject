#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shop.BasketService.Api/Shop.BasketService.Api.csproj", "Shop.BasketService.Api/"]
COPY ["Shop.BasketService.Business/Shop.BasketService.Business.csproj", "Shop.BasketService.Business/"]
COPY ["Shop.BasketService.Entities/Shop.BasketService.Entities.csproj", "Shop.BasketService.Entities/"]
COPY ["Shop.Domain/Shop.Domain.csproj", "Shop.Domain/"]
COPY ["Shop.Core/Shop.Core.csproj", "Shop.Core/"]
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