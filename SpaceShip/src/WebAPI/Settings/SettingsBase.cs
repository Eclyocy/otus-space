using System;

namespace SpaceShip.WebAPI.Settings;

/// <summary>
/// Базовый класс для получения переменных
/// </summary>
public class SettingsBase
{
    /// <summary>
    /// Метод получения данных из переменных окружения
    /// </summary>
    /// <param name="variableName">Имя переменной</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Если переменная не задана или имеет значение NULL</exception>
    protected static string GetEnvironmentVariableValue(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName) ?? throw new ArgumentNullException();
    }
}