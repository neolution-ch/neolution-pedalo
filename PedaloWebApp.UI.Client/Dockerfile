FROM node:22-alpine3.20 AS build

# Needed for base64 -w option...
# ref: https://stackoverflow.com/a/57921202
RUN apk add --update coreutils

# Setting working directory in the container. All the path will be relative to WORKDIR
# this is where we deploy the data
WORKDIR /usr/src/app

# Installing dependencies
COPY PedaloWebApp.UI.Client/package.json ./
COPY PedaloWebApp.UI.Client/yarn.lock ./
COPY PedaloWebApp.UI.Client/.npmrc ./
COPY PedaloWebApp.UI.Client/.yarnrc.yml ./

# restore dependencies
RUN corepack enable
RUN yarn --frozen-lockfile

# Copying source files
COPY PedaloWebApp.UI.Client/ .
COPY PedaloWebApp.UI.Api/openapi-spec.json ../PedaloWebApp.UI.Api/openapi-spec.json

#Write the build id from azure dev ops to the version.json file
ARG BUILDID=default
RUN echo "{ \"buildId\": \"${BUILDID}\" }" > version.json

RUN yarn orval
RUN yarn prettier . --check
RUN yarn lint
RUN yarn build

FROM node:22-alpine3.20 AS final

RUN apk --no-cache add curl

WORKDIR /usr/src/app

ENV PORT=80
EXPOSE 80

COPY --from=build /usr/src/app/. .

# Running the app
ENTRYPOINT ["yarn"]
CMD ["next", "start"]
