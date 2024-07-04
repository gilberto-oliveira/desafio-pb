#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y curl
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/DesafioPB.Common/DesafioPB.Common.csproj", "DesafioPB.Common/"]
COPY ["src/DesafioPB.CartaoDeCredito/DesafioPB.CartaoDeCredito.csproj", "DesafioPB.CartaoDeCredito/"]

RUN dotnet restore "DesafioPB.CartaoDeCredito/DesafioPB.CartaoDeCredito.csproj"
COPY . .

WORKDIR "./src/DesafioPB.CartaoDeCredito"
RUN dotnet build "DesafioPB.CartaoDeCredito.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioPB.CartaoDeCredito.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioPB.CartaoDeCredito.dll"]