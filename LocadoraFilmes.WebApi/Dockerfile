#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LocadoraFilmes.WebApi/LocadoraFilmes.WebApi.csproj", "LocadoraFilmes.WebApi/"]
COPY ["Infra/LocadoraFilmes.Infra.IoC.csproj", "Infra/"]
COPY ["LocadoraFilmes.Infra.Data/LocadoraFilmes.Infra.Data.csproj", "LocadoraFilmes.Infra.Data/"]
COPY ["LocadoraFilmes.Domain/LocadoraFilmes.Domain.csproj", "LocadoraFilmes.Domain/"]
COPY ["LocadoraFilmes.Application/LocadoraFilmes.Application.csproj", "LocadoraFilmes.Application/"]
RUN dotnet restore "LocadoraFilmes.WebApi/LocadoraFilmes.WebApi.csproj"
COPY . .
WORKDIR "/src/LocadoraFilmes.WebApi"
RUN dotnet build "LocadoraFilmes.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocadoraFilmes.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocadoraFilmes.WebApi.dll"]