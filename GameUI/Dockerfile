FROM node:22-alpine

RUN mkdir /app
WORKDIR /app

RUN npm install -g @angular/cli@18

COPY package.json package-lock.json ./
RUN npm ci

COPY . .
CMD ["ng", "serve", "--host", "0.0.0.0"]
