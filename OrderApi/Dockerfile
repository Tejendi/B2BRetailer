﻿FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 8020

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
#COPY OrderApi.sln ./
COPY OrderApi/OrderApi.csproj /src
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/OrderApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderApi.dll"]
