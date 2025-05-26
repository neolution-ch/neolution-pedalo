FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Timezone data
# RUN apk add --update tzdata

# Workaround Unhandled exception. System.NotSupportedException: Globalization Invariant Mode is not supported.
# https://github.com/dotnet/SqlClient/issues/220
# RUN apk add --update icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# If curl fails to download the credential provider, try to force ipv4 by uncommenting next line
# RUN echo '--ipv4' >> ~/.curlrc

WORKDIR /src

# Copy all the code
COPY . .

RUN for p in $(find . -name *.UnitTests.csproj); do dotnet restore --locked-mode $p; done 
RUN for p in $(find . -name *.UnitTests.csproj); do dotnet build -c Release $p; done 
RUN ls -l

ENTRYPOINT ["dotnet", "test", "-c", "Release", "--no-build", "--results-directory", "/testresults", "--logger", "trx", "--collect:\"XPlat Code Coverage\""]