#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1803 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1803 AS build
WORKDIR /src
COPY ["eCommerce.Api.Products.Tests/eCommerce.Api.Products.Tests.csproj", "eCommerce.Api.Products.Tests/"]
COPY ["eCommerce.Api.Products/eCommerce.Api.Products.csproj", "eCommerce.Api.Products/"]
RUN dotnet restore "eCommerce.Api.Products.Tests/eCommerce.Api.Products.Tests.csproj"
COPY . .
WORKDIR "/src/eCommerce.Api.Products.Tests"
RUN dotnet build "eCommerce.Api.Products.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Api.Products.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Api.Products.Tests.dll"]