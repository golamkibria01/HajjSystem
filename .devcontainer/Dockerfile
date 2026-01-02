FROM mcr.microsoft.com/devcontainers/dotnet:8.0

# Install PostgreSQL client tools
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends postgresql-client \
    && apt-get clean -y && rm -rf /var/lib/apt/lists/*

# Install Entity Framework Core tools with specific version
RUN dotnet tool install --global dotnet-ef --version 8.0.*

# Add dotnet tools to PATH
ENV PATH="${PATH}:/home/vscode/.dotnet/tools"
