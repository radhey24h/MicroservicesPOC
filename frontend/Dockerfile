# pull official base image for Node.js 22
FROM node:22-alpine as builder

# set working directory
WORKDIR /usr/src/app

# add `usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# copy necessary files for building the app
COPY public/ ./public
COPY src/ ./src
COPY package.json ./
COPY package-lock.json ./

# install and cache app dependencies
RUN npm ci --loglevel verbose

# argument for environment
ARG FRONTEND_APP_CLIENT_NAME
ARG FRONTEND_APP_WEB_API_BASE_URL
ARG FRONTEND_APP_WEB_API_PORT
ARG FRONTEND_APP_ENVIRONMENT

ENV FRONTEND_APP_CLIENT_NAME=${FRONTEND_APP_CLIENT_NAME}
ENV FRONTEND_APP_WEB_API_BASE_URL=${FRONTEND_APP_WEB_API_BASE_URL}
ENV FRONTEND_APP_WEB_API_PORT=${FRONTEND_APP_WEB_API_PORT}
ENV FRONTEND_APP_ENVIRONMENT=${FRONTEND_APP_ENVIRONMENT}

ARG envname
ENV env_name=$envname
RUN echo "The Environment variable is ${env_name}"

# create build
COPY . ./
RUN npm run build
#:${env_name}

#########
# FINAL #
#########

# pull official base image for nginx
FROM nginx:alpine

# update nginx conf
RUN rm -rf /etc/nginx/conf.d
COPY conf /etc/nginx

# copy static files from builder stage
COPY --from=builder /usr/src/app/build /usr/share/nginx/html

# set permissions for static files
RUN find /usr/share/nginx/html -type d -exec chmod 755 {} +
RUN find /usr/share/nginx/html -type f -exec chmod 755 {} +
RUN find /usr/share/nginx/html -type d -exec chmod +x {} +

# expose port
EXPOSE 80
EXPOSE 443

# run nginx
CMD ["nginx", "-g", "daemon off;"]