using System.Text;
using System.Text.Json;
using GameController.Services.Interfaces;
using GameController.Services.Models.Message;
using GameController.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service for working with Rabbit MQ.
    /// </summary>
    public class RabbitMQService : IRabbitMQService
    {
        #region private fields

        private readonly RabbitMQSettings _rabbitMQSettings;

        private readonly ILogger<RabbitMQService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RabbitMQService(
            ILogger<RabbitMQService> logger,
            IOptions<RabbitMQSettings> options)
        {
            _logger = logger;

            _rabbitMQSettings = options.Value;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public void SendNewDayMessage(NewDayMessage newDayMessage)
        {
            _logger.LogInformation("Send 'new day' message to Rabbit MQ: {message}", newDayMessage);

            ConnectionFactory connectionFactory = new()
            {
                HostName = _rabbitMQSettings.Hostname,
                Port = _rabbitMQSettings.Port,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password,
                VirtualHost = _rabbitMQSettings.VirtualHost
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newDayMessage));

            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            channel.BasicPublish(
                exchange: _rabbitMQSettings.NewDayExchangeName,
                routingKey: string.Empty,
                mandatory: false,
                basicProperties: basicProperties,
                body: body);
        }

        #endregion
    }
}
