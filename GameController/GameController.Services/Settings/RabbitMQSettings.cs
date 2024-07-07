namespace GameController.Services.Settings
{
    /// <summary>
    /// Settings for Rabbit MQ service.
    /// </summary>
    public class RabbitMQSettings
    {
        #region private constants

        private const string ExceptionTemplate = "{0} environment variable must be specified";

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RabbitMQSettings()
        {
            Hostname = GetEnvironmentVariable("RABBITMQ_HOSTNAME");

            string port = GetEnvironmentVariable("RABBITMQ_PORT");
            Port = int.Parse(port);

            Username = GetEnvironmentVariable("RABBITMQ_USERNAME");
            Password = GetEnvironmentVariable("RABBITMQ_PASSWORD");
            VirtualHost = GetEnvironmentVariable("RABBITMQ_VIRTUALHOST");
            NewDayExchange = GetEnvironmentVariable("EXCHANGENAME_NEWDAY");
            NewDayQueueGenerator = GetEnvironmentVariable("QUEUENAME_NEWDAY_GENERATOR");
            NewDayQueueShip = GetEnvironmentVariable("QUEUENAME_NEWDAY_SHIP");
        }

        #endregion

        #region public properties

        /// <summary>
        /// Rabbit MQ service hostname.
        /// </summary>
        public string Hostname { get; }

        /// <summary>
        /// Rabbit MQ service port.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Rabbit MQ service username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Rabbit MQ service password.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Rabbit MQ virtual host.
        /// </summary>
        public string VirtualHost { get; }

        /// <summary>
        /// Rabbit MQ exchange name for "new day" messages.
        /// </summary>
        public string NewDayExchange { get; }

        /// <summary>
        /// Rabbit MQ queue name for "new day" messages for generator.
        /// </summary>
        public string NewDayQueueGenerator { get; }

        /// <summary>
        /// Rabbit MQ queue name for "new day" messages for ship.
        /// </summary>
        public string NewDayQueueShip { get; }

        #endregion

        #region private methods

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name)
                ?? throw new Exception(string.Format(ExceptionTemplate, name));
        }

        #endregion
    }
}
