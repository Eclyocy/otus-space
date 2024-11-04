using SpaceShip.Service.Extensions;

namespace SpaceShip.Service.Helpers;

/// <summary>
/// Helper for space ship names generation.
/// </summary>
public static class RandomNameGenerator
{
    #region private fields

    private const string Delimiter = " ";

    private static readonly List<string> _adjectives = [
        "Разящий",
        "Красный",
        "Решительный",
        "Благолепный",
        "Отважный",
        string.Empty
    ];

    private static readonly List<string> _nouns = [
        "Стриж",
        "Партизан",
        "Сокол",
        "Варяг",
        string.Empty
    ];

    #endregion

    #region public methods

    /// <summary>
    /// Generate a space ship name.
    /// </summary>
    public static string Get()
    {
        string adjective = _adjectives.PickRandom();
        string noun = _nouns.PickRandom();

        string delimiter = (!string.IsNullOrEmpty(adjective) && !string.IsNullOrEmpty(noun))
            ? Delimiter
            : string.Empty;

        return adjective + delimiter + noun;
    }

    #endregion
}
