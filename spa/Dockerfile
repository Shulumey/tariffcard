### STAGE 1: Build ###

# We label our stage as ‘builder’
FROM registry.gitlab.com/industrysoft/image/node:14-alpine as builder

WORKDIR /ng-app

COPY package.json package-lock.json .npmrc ./

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm ci
RUN node ./node_modules/.bin/ngcc

COPY ./.browserslistrc ./.browserslistrc
COPY ./.editorconfig ./.editorconfig
COPY ./angular.json ./angular.json

COPY ./tsconfig.app.json ./tsconfig.app.json
COPY ./tsconfig.json ./tsconfig.json
COPY ./tsconfig.spec.json ./tsconfig.spec.json

COPY ./.eslintrc.json ./.eslintrc.json

COPY ./src/ ./src/

## Run eslint

RUN npm run lint

## Build the angular app in production mode and store the artifacts in dist folder

RUN npm run ng build -- --prod

# Rename default webpackJsonp to avoid an issue with intersection with another webpack module loader
RUN find dist -type f -iname "*.js" -exec sed -ri "s@webpackJsonp@tcWebpackJsonp@gi" {} \;

# replace /assets/<path> => /tariffcard/assets/<path>
RUN find dist -type f -exec sed -ri "s@\/assets\/|assets\/@\/tariffcard\/assets\/@gi" {} \;

### STAGE 2: Setup ###
FROM registry.gitlab.com/industrysoft/image/nginx:1.21.3-alpine

## From ‘builder’ stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=builder /ng-app/dist /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]

# ! docker image build -t tariffcard-spa:0.1 .
