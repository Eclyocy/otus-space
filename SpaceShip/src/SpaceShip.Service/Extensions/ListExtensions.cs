namespace SpaceShip.Service.Extensions;

/// <summary>
/// Extensions for <see cref="List{T}"/>.
/// </summary>
public static class ListExtensions
{
    #region pubic methods

    /// <summary>
    /// Picks a random element from the <paramref name="list"/>.
    /// </summary>
    /// <typeparam name="T">Type of list elements.</typeparam>
    public static T PickRandom<T>(this List<T> list)
    {
        Random random = new();

        int index = random.Next(list.Count);

        return list[index];
    }

    #endregion
}
