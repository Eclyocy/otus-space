FROM node:22-alpine

# Get curl for healthcheck
RUN apk add --no-cache curl

RUN mkdir /app
WORKDIR /app

RUN npm install -g @angular/cli@18

COPY package.json package-lock.json ./
RUN npm ci

COPY . .
CMD ["ng", "serve", "--host", "0.0.0.0"]

EXPOSE 4200
