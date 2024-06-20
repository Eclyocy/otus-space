namespace SessionController.Services
{
    /// <summary>
    /// Интерфейс для работы с игровыми сессиями.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Создать новую сессию для игрока <paramref name="userId"/>.
        /// </summary>
        public void CreateUserSession(Guid userId);

        /// <summary>
        /// Получить список <see cref="Guid"/>'ов всех сессий для игрока <paramref name="userId"/>.
        /// </summary>
        public List<Guid> GetUserSessions(Guid userId);

        /// <summary>
        /// Получить информацию о конкретной сессии <paramref name="sessionId"/>
        /// игрока <paramref name="userId"/>.
        /// </summary>
        /// <remarks>
        /// TODO: информация, полученная с Корабля: метрики и текущий ход.
        /// Сейчас -- заглушка с "текущим ходом".
        /// </remarks>
        public int GetUserSessionInformation(Guid userId, Guid sessionId);
    }
}
