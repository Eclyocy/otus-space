# Space Project: RabbitMQ

RabiitMQ broker is needed for exchanging messages between the standalone web-servises.

## Installation

### Requirements

Docker and docker-compose (see [official documentation](https://www.docker.com/products/docker-desktop/) for installation).

### Deployment

Run this command from the directory you have cloned this repository to:
```
docker-compose up -d
```

## Configuration

### Users

| name | password | is admin |
| -- | -- | -- |
| guest | guest | + |
| game-controller | Gc1234 | - |
| problem-generator | Pg1234 | - |
| space-ship | Ss1234 | - |
