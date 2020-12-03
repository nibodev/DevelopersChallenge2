FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY nuget.docker ./nuget.config
COPY Poll ./POLL

WORKDIR /app/POLL/src/Poll.Web
RUN dotnet restore
RUN dotnet publish -c Release -o /app/POLL/out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/POLL/out .
ENTRYPOINT ["dotnet", "Poll.Web.dll"]
