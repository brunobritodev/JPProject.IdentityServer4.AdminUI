# base image
FROM node:alpine as builder

# set working directory
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY ["Frontend/Jp.UserManagement/package.json", "/usr/src/app"]
COPY ["Frontend/Jp.UserManagement/package-lock.json", "/usr/src/app"]
RUN npm install
RUN npm install -g @angular/cli@1.7.1 --unsafe

# add app
COPY ["Frontend/Jp.UserManagement/", "/usr/src/app"]

# generate build
RUN npm run build

##################
### production ###
##################

# base image
FROM nginx:1.15.3-alpine

# copy artifact build from the 'build environment'
COPY --from=builder /usr/src/app/dist /usr/share/nginx/html
COPY ["Frontend/Jp.UserManagement/nginx/nginx.conf", "/etc/nginx/nginx.conf"]
WORKDIR /usr/share/nginx/html

# expose port 80
EXPOSE 80

# run nginx
CMD ["nginx", "-g", "daemon off;"]
