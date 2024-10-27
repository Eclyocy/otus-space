using SpaceShip.Service.Helpers.Abstractions;

namespace SpaceShip.Service.Helpers;

/// <inheritdoc/>
public class RandomNameGenerator : INameGenerator
{
    private static readonly List<string> _description = ["Разящий", "Красный", "Решительный", "Благолепный", "Отважный", string.Empty];
    private static readonly List<string> _species = ["Стриж", "Партизан", "Сокол", "Варяг", string.Empty];

    /// <inheritdoc/>
    public string Get()
    {
        Random rnd = new();
        var indx = rnd.Next(_description.Count);
        var description = _description[indx];

        indx = rnd.Next(_description.Count);
        var species = _species[indx];

        var delimiter = string.Empty;
        if ((description != string.Empty) || (species != string.Empty))
        {
            delimiter = " ";
        }

        return description + delimiter + species;
    }
}
