# Библиотека с общими ресурсами и утилитами

## Enums

Набор перечислений типов ресурсов и их состояний.

### EventLevel

[EventLevel.cs](./Enums/EventLevel.cs)

Уровни критичности проблем от генератора событий:
- Low
- Medium
- High

### ResourceState

[ResourceState.cs](./Enums/ResourceState.cs)

Возможные состояния ресурсов и компонентов корабля:
- Ok &mdash; нормальное состояние. Ресурс в работе или может быть использован в любой момент. На поддержание этого состояния потребляются зависимые ресурсы каждый игровой день.
- Sleep &mdash; ресурс исправен, но не активен. Ресурсы на поддержание этого состояния не расходуются.
- Fail &mdash; ресурс повреждён, использовать его нельзя. На поддержание состояния другие ресурсы не используются, но могут быть запрошены на восстановление состояния до рабочего (Ok).

### ResourceType

[ResourceType.cs](./Enums/ResourceType.cs)

Типы ресурсов на корабле.

### ShipState

[ShipState.cs](./Enums/ShipState.cs)

Набор возможных состояний корабля:
- OK &mdash; нормальное рабочее состояние, полёт возможен или проходит в штатном режиме.
- Adrift &mdash; корабль лег в дрейф, в штатном реиме полет невозможен, например из-за нехватки топлива или неисправности двигателя. Возможен переход в нормальное состояние.
- Crashed &mdash; корабль разбился, нет возможности завершить миссию, например повреждён корпус и ресурсов для ремонта недостаточно. Миссия провалена.
- Arrived &mdash; корабль прибыл в пункт назначения. За отведённое игровое время (число дней - шагов игры) хватило ресурсов на полет (поддержание нормального состояния) и устранение неисправностей от генератора проблем. Миссия успешно выполнена.

## Utilities

### Key-Based Lock

[KeyBasedLock.cs](./Utilities/KeyBasedLock.cs)

Реализован как IDisposable, полагается на ConcurrentDictionary:
- ключ — это идентификатор объекта
- значение — SemaphoreSlim(1, 1)

#### Пример использования

Пример использования в [UserService](../GameController/GameController.Services/Services/UserService.cs)

```
    // объявление в качестве static поля
    private static readonly KeyBasedLock<Guid> _semaphoreLock = new();

    ...

    using (await _semaphoreLock.LockAsync(userId, _logger, CancellationToken.None))
    {
        ...
        User user = UpdateRepositoryUser(userId, updateUserDto);
        ...
    }
```
