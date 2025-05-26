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

COPY ["PedaloWebApp.UI.Api/PedaloWebApp.UI.Api.csproj", "PedaloWebApp.UI.Api/packages.lock.json", "PedaloWebApp.UI.Api/"]
RUN dotnet restore --locked-mode "PedaloWebApp.UI.Api/PedaloWebApp.UI.Api.csproj"

# Copy the rest of the code (we only copy the nesecary files to make good use of layer caching)
COPY PedaloWebApp.Core/ PedaloWebApp.Core
COPY PedaloWebApp.Infrastructure/ PedaloWebApp.Infrastructure
COPY PedaloWebApp.UI.Api/ PedaloWebApp.UI.Api

WORKDIR "/src/PedaloWebApp.UI.Api"
# build the project in release mode
RUN dotnet build "PedaloWebApp.UI.Api.csproj" -c Release

FROM build AS publish
RUN dotnet publish "PedaloWebApp.UI.Api.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

RUN apk --no-cache add curl
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=docker.io/neotrow/grpc-build /out/libgrpc_csharp_ext.so /app/libgrpc_csharp_ext.x64.so
COPY --from=build /src/PedaloWebApp.UI.Api/openapi-spec.json /app/spec.json

ENTRYPOINT ["dotnet", "PedaloWebApp.UI.Api.dll"]
