FROM nginx:latest
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
RUN mkdir -p /var/www/cache

CMD ["nginx", "-g", "daemon off;"]
