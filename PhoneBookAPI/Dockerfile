#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PhoneBookAPI.csproj", ""]
RUN dotnet restore "./PhoneBookAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PhoneBookAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneBookAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBookAPI.dll"]