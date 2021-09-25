FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TaskManager.Client/TaskManager.Client.csproj", "TaskManager.Client/"]
RUN dotnet restore "TaskManager.Client/TaskManager.Client.csproj"
COPY . .
WORKDIR "/src/TaskManager.Client"
RUN dotnet build "TaskManager.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.Client.dll"]
