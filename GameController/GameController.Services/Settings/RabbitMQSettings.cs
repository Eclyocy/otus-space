namespace GameController.Services.Settings
{
    /// <summary>
    /// Settings for Rabbit MQ service.
    /// </summary>
    public class RabbitMQSettings
    {
        #region public properties

        /// <summary>
        /// Rabbit MQ service hostname.
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Rabbit MQ service port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Rabbit MQ service username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Rabbit MQ service password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rabbit MQ virtual host.
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// Rabbit MQ exchange name for "new day" messages.
        /// </summary>
        public string NewDayExchangeName { get; set; }

        /// <summary>
        /// Rabbit MQ queue name for "new day" messages for generator.
        /// </summary>
        public string NewDayQueueNameGenerator { get; set; }

        #endregion
    }
}
