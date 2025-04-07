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
COPY ["Pedalo.Core/Pedalo.Core.csproj", "Pedalo.Core/packages.lock.json", "Pedalo.Core/"]
COPY ["Pedalo.Infrastructure/Pedalo.Infrastructure.csproj", "Pedalo.Infrastructure/packages.lock.json", "Pedalo.Infrastructure/"]

# restore the projects (this will be very fast with layer caching if the csproj files haven't changed)
RUN dotnet restore --locked-mode "Pedalo.Core/Pedalo.Core.csproj"
RUN dotnet restore --locked-mode "Pedalo.Infrastructure/Pedalo.Infrastructure.csproj"

COPY ["Pedalo.UI.Api/Pedalo.UI.Api.csproj", "Pedalo.UI.Api/packages.lock.json", "Pedalo.UI.Api/"]
RUN dotnet restore --locked-mode "Pedalo.UI.Api/Pedalo.UI.Api.csproj"

# Copy the rest of the code (we only copy the nesecary files to make good use of layer caching)
COPY Pedalo.Core/ Pedalo.Core
COPY Pedalo.Infrastructure/ Pedalo.Infrastructure
COPY Pedalo.UI.Api/ Pedalo.UI.Api

WORKDIR "/src/Pedalo.UI.Api"
# build the project in release mode
RUN dotnet build "Pedalo.UI.Api.csproj" -c Release

FROM build AS publish
RUN dotnet publish "Pedalo.UI.Api.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

RUN apk --no-cache add curl
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=docker.io/neotrow/grpc-build /out/libgrpc_csharp_ext.so /app/libgrpc_csharp_ext.x64.so
COPY --from=build /src/Pedalo.UI.Api/openapi-spec.json /app/spec.json

ENTRYPOINT ["dotnet", "Pedalo.UI.Api.dll"]
