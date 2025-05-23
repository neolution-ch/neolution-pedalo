FROM cypress/browsers:node18.12.0-chrome106-ff106

# Setting working directory in the container. All the path will be relative to WORKDIR
# this is where we deploy the data
WORKDIR /usr/src/app

# expose the port to external - needs to be port 80 for azure
EXPOSE 80

# Installing dependencies
COPY PedaloWebApp.FunctionalTests/package.json ./
COPY PedaloWebApp.FunctionalTests/yarn.lock ./
COPY PedaloWebApp.FunctionalTests/.yarnrc.yml ./

# restore dependencies
RUN corepack enable
RUN yarn --frozen-lockfile

# Copying source files
COPY PedaloWebApp.FunctionalTests/ .
COPY PedaloWebApp.UI.Api/openapi-spec.json ../PedaloWebApp.UI.Api/openapi-spec.json

RUN yarn orval
RUN yarn prettier . --check
RUN yarn lint
RUN yarn build

# Running the app
ENTRYPOINT ["yarn"]
CMD ["cypress", "run", "-b", "firefox"]
