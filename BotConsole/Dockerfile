FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BotConsole/BotConsole.csproj", "BotConsole/"]
RUN dotnet restore "BotConsole/BotConsole.csproj"
COPY . .
WORKDIR "/src/BotConsole"
RUN dotnet build "BotConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BotConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotConsole.dll"]
