#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DevelopersChallenge2.Application/DevelopersChallenge2.Application.csproj", "DevelopersChallenge2.Application/"]
RUN dotnet restore "DevelopersChallenge2.Application/DevelopersChallenge2.Application.csproj"
COPY . .
WORKDIR "/src/DevelopersChallenge2.Application"
RUN dotnet build "DevelopersChallenge2.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevelopersChallenge2.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevelopersChallenge2.Application.dll"]