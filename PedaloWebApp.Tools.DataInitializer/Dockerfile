#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

# Install Timezone data
RUN apk add --update tzdata

# Workaround Unhandled exception. System.NotSupportedException: Globalization Invariant Mode is not supported.
# https://github.com/dotnet/SqlClient/issues/220
RUN apk add --update icu-libs
RUN apk add icu-data-full
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# If curl fails to download the credential provider, try to force ipv4 by uncommenting next line
# RUN echo '--ipv4' >> ~/.curlrc

WORKDIR /src

# Copy solution-wide files
COPY .editorconfig .
COPY Directory.Build.props .
COPY nuget.config .

# We only copy the csproj files first so the layer caching will work correctly
COPY ["PedaloWebApp.Core/PedaloWebApp.Core.csproj", "PedaloWebApp.Core/packages.lock.json", "PedaloWebApp.Core/"]
COPY ["PedaloWebApp.Infrastructure/PedaloWebApp.Infrastructure.csproj", "PedaloWebApp.Infrastructure/packages.lock.json", "PedaloWebApp.Infrastructure/"]

# restore the projects (this will be very fast with layer caching if the csproj files haven't changed)
RUN dotnet restore --locked-mode "PedaloWebApp.Core/PedaloWebApp.Core.csproj"
RUN dotnet restore --locked-mode "PedaloWebApp.Infrastructure/PedaloWebApp.Infrastructure.csproj"

COPY ["PedaloWebApp.Tools.DataInitializer/PedaloWebApp.Tools.DataInitializer.csproj", "PedaloWebApp.Tools.DataInitializer/packages.lock.json", "PedaloWebApp.Tools.DataInitializer/"]
RUN dotnet restore --locked-mode "PedaloWebApp.Tools.DataInitializer/PedaloWebApp.Tools.DataInitializer.csproj"

# Copy the rest of the code (we only copy the nesecary files to make good use of layer caching)
COPY PedaloWebApp.Core/ PedaloWebApp.Core
COPY PedaloWebApp.Infrastructure/ PedaloWebApp.Infrastructure
COPY PedaloWebApp.Tools.DataInitializer/ PedaloWebApp.Tools.DataInitializer

RUN ls /src/PedaloWebApp.Core

WORKDIR "/src/PedaloWebApp.Tools.DataInitializer"
# build the project in release mode
RUN dotnet build "PedaloWebApp.Tools.DataInitializer.csproj" -c Release

FROM build AS publish
RUN dotnet publish "PedaloWebApp.Tools.DataInitializer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PedaloWebApp.Tools.DataInitializer.dll"]