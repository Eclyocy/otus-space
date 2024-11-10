# Микросервис генератора событий

## Задачи

- Хранит и управляет информацией о генераторах и их событиях.
- Consumer сообщений «новый день» (от игрового контроллера).
- Producer сообщений «событие».

## Deploy

### Dockerfile

[Dockerfile](./Dockerfile)

### Переменные окружения

| Имя переменной                | Используемое значение | Описание                                                            |
|-------------------------------|-----------------------|---------------------------------------------------------------------|
| ASPNETCORE_ENVIRONMENT        | Production            | Тип окружения развёрнутого приложения (Development или Production). |
| ASPNETCORE_URLS               | http://+:7161         | Список URL, на которых будет запущено приложение.                   |
| DATABASE_HOSTNAME             | game_controller_db    | Адрес хоста PostgreSQL.                                             |
| DATABASE_PORT                 | 5432                  | Порт для подключения к PostgreSQL.                                  |
| DATABASE_NAME                 | event_generator_db    | Имя БД PostgreSQL.                                                  |
| DATABASE_USER                 | postgres              | Имя пользователя для подключения к БД.                              |
| DATABASE_PASSWORD             | pg_pass               | Пароль подключения к БД.                                            |
| RABBITMQ_HOST                 | rabbitmq              | Адрес хоста брокера сообщений RabbitMQ.                             |
| RABBITMQ_USER                 | problem-generator     | Имя пользователя для подключения к RabbitMQ.                        |
| RABBITMQ_PASSWORD             | Pg1234                | Пароль для подключения к RabbitMQ.                                  |
| RABBITMQ_NEW_DAY_EXCHANGE     | x_new_day             | Exchange для привязки очереди сообщений о новом игровом дне.        |
| RABBITMQ_NEW_DAY_QUEUE        | q_new_day_generator   | Имя очереди для событий "новый день" на RabbitMQ для SpaceShip.     |
| RABBITMQ_EVENTS_EXCHANGE      | x_troubles            | Exchange для привязки очереди сообщений о событиях для SpaceShip.   |

## Tests

[GitHub action](../.github/workflows/EventGeneratorCI.yml)

## Endpoints

### Healthcheck

Реализован стандартный endpoint для проверки работоспособности сервиса: `/health`.

### Методы WebAPI

Более подробно описаны в swagger.

- POST `/api/generators` &mdash; создать новый генератор событий
- GET `/api/generators/{generatorId}` &mdash; получить существующий генератор событий
- POST `/api/generators/{generatorId}/coins` &mdash; добавить генератору событий монетку
- GET `/api/generators/{generatorId}/events` &mdash; получить все события, сгенерированные существующим генератором
- POST `/api/generators/{generatorId}/events` &mdash; создать новое событие

## Схема БД

![database diagram](./docs/database.png)
