# GameUI

Пользовательский интерфейс, написанный при помощи [Angular CLI](https://github.com/angular/angular-cli) 18-ой версии.

## Архитектура

Состоит из standalone-компонент, подгружаемых в [общий компонент приложения](./src/app/app.component.ts):
- компонент [для списка пользователей](./src/app/components/users/);
- компонент [страницы пользователя](./src/app/components/user/);
- компонент [конкретного корабля](./src/app/components/ship/);
- компонент [страницы login](./src/app/components/login/).

### Авторизация

Для возможности автоматического продления JWT используется [interceptor](./src/app/interceptors/auth.interceptor.ts).

JWT хранится в локальном хранилище. Работа с JWT описана в [auth-сервисе](./src/app/services/auth.service.ts).

### SpaceShip SignalR

Для автоматического обновления метрик космических кораблей используется [SignalR-сервис](./src/app/services/ship.signalr.service.ts).

### GameController API

Для обращения к эндпоинтам игрового контроллера используется [API-сервис](./src/app/services/api.service.ts).

## Разработка

### Генерация компонентов

- `ng generate component component-name` &mdash; для генерации нового компонента
- `ng generate directive|pipe|service|class|guard|interface|enum|module` &mdash; для генерации прочих объектов

### Локальный запуск

- `npm install` &mdash; установка зависимостей.
- `ng build` &mdash; сборка проекта.
- `ng serve` &mdash; развёртывание сервера, по умолчанию на `http://localhost:4200/`<br/>Приложение автоматически подгружает изменения в исходных файлах.
