FROM node:22-alpine

# Get curl for healthcheck
RUN apk add --no-cache curl

RUN mkdir /app
WORKDIR /app

#RUN npm i -g npm@latest
RUN npm i -g npm@10.8.3
RUN npm i -g @angular/cli@18 --verbose

COPY package.json package-lock.json ./
RUN npm ci --verbose

COPY . .
CMD ["ng", "serve", "--host", "0.0.0.0"]

EXPOSE 4200
