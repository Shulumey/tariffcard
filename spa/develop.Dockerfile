FROM registry.gitlab.com/industrysoft/image/node:14-alpine as builder

WORKDIR /ng-app

COPY package.json package-lock.json .npmrc ./

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm ci
RUN node ./node_modules/.bin/ngcc

# #############################################################################
# Application Code
#
COPY . .

# #############################################################################
# Expose
#
EXPOSE 4200

# #############################################################################
# Start dev server with polling for Windows
#
ENTRYPOINT ["/bin/sh", "-c", "if [ \"$ENABLE_POLLING\" = \"enabled\" ]; \
    then npm run start:docker:poll; else npm run start:docker; fi"]
