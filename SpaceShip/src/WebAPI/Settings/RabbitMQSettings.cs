#pragma warning disable CS1591

namespace SpaceShip.WebAPI.Settings;
public class RabbitMQSettings: SettingsBase
{
    public string Host {get; set;}

    public string Vhost {get; set;}

    public string User {get; set;}

    public string Password {get; set;}

    public string QueueNameStep {get; set;}

    public string QueueNameTrouble {get; set;}

    #region constructor

    public RabbitMQSettings()
    {
        Host = GetEnvironmentVariableValue("RABBITMQ_HOST");
        Vhost = GetEnvironmentVariableValue("RABBITMQ_VHOST");
        User = GetEnvironmentVariableValue("RABBITMQ_USER");
        Password = GetEnvironmentVariableValue("RABBITMQ_PASSWORD");
        QueueNameStep = GetEnvironmentVariableValue("RABBITMQ_STEP_QUEUE");
        QueueNameTrouble = GetEnvironmentVariableValue("RABBITMQ_TROUBLES_QUEUE");

    }

    #endregion


}