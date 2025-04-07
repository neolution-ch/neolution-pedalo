FROM cypress/browsers:node18.12.0-chrome106-ff106

# Setting working directory in the container. All the path will be relative to WORKDIR
# this is where we deploy the data
WORKDIR /usr/src/app

# expose the port to external - needs to be port 80 for azure
EXPOSE 80

# Installing dependencies
COPY Pedalo.FunctionalTests/package.json ./
COPY Pedalo.FunctionalTests/yarn.lock ./

# restore dependencies
RUN yarn --frozen-lockfile

# Copying source files
COPY Pedalo.FunctionalTests/ .
COPY Pedalo.UI.Api/openapi-spec.json ../Pedalo.UI.Api/openapi-spec.json

RUN yarn orval
RUN yarn prettier . --check
RUN yarn lint
RUN yarn build

# Running the app
ENTRYPOINT ["yarn"]
CMD ["cypress", "run", "-b", "firefox"]
