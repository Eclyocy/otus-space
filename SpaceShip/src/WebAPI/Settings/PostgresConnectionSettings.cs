#pragma warning disable CS1591

namespace SpaceShip.WebAPI.Settings;

public class PostgresConnectionSettings : SettingsBase
{
    public string? PostgresConnectionString {get; set;}

}