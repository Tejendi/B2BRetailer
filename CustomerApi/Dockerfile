﻿FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 8030

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
#COPY CustomerApi.sln ./
COPY CustomerApi/CustomerApi.csproj /src
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/CustomerApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]
