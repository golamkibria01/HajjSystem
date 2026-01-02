FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY ["HajjSystem.sln", "./"]

# Copy all project files
COPY ["Sources/HajjSystem.Models/HajjSystem.Models.csproj", "Sources/HajjSystem.Models/"]
COPY ["Sources/HajjSystem.Data/HajjSystem.Data.csproj", "Sources/HajjSystem.Data/"]
COPY ["Sources/HajjSystem.Services/HajjSystem.Services.csproj", "Sources/HajjSystem.Services/"]
COPY ["Sources/HajjSystem.Webapi/HajjSystem.Webapi.csproj", "Sources/HajjSystem.Webapi/"]

# Restore dependencies
RUN dotnet restore "Sources/HajjSystem.Webapi/HajjSystem.Webapi.csproj"

# Copy everything else
COPY . .

# Build the project
WORKDIR "/src/Sources/HajjSystem.Webapi"
RUN dotnet build "HajjSystem.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HajjSystem.Webapi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HajjSystem.Webapi.dll"]
