using System.Text;
using System.Text.Json;
using GameController.Services.Interfaces;
using GameController.Services.Models.Message;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with Rabbit MQ.
    /// </summary>
    public class RabbitMQService : IRabbitMQService
    {
        #region private fields

        private readonly string? _rabbitMQHostname;
        private readonly int? _rabbitMQPort;
        private readonly string? _rabbitMQUsername;
        private readonly string? _rabbitMQPassword;
        private readonly string? _rabbitMQVirtualHost;

        private readonly string? _newDayExchangeName;
        private readonly string? _newDayGeneratorQueueName;
        private readonly string? _newDayShipQueueName;

        private readonly ILogger<RabbitMQService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RabbitMQService(
            ILogger<RabbitMQService> logger,
            IConfiguration configuration)
        {
            _logger = logger;

            _rabbitMQHostname = configuration["RABBITMQ_HOSTNAME"];
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            _rabbitMQPort = int.Parse(configuration["RABBITMQ_PORT"]);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            _rabbitMQUsername = configuration["RABBITMQ_USERNAME"];
            _rabbitMQPassword = configuration["RABBITMQ_PASSWORD"];
            _rabbitMQVirtualHost = configuration["RABBITMQ_VIRTUALHOST"];

            _newDayExchangeName = configuration["EXCHANGENAME_NEWDAY"];
            _newDayGeneratorQueueName = configuration["QUEUENAME_NEWDAY_GENERATOR"];
            _newDayShipQueueName = configuration["QUEUENAME_NEWDAY_SHIP"];
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public void SendNewDayMessage(NewDayMessage newDayMessage)
        {
            _logger.LogInformation("Send 'new day' message to Rabbit MQ: {message}", newDayMessage);

            ConnectionFactory connectionFactory = new()
            {
                HostName = _rabbitMQHostname,
               // Port = _rabbitMQPort?.Value,
                UserName = _rabbitMQUsername,
                Password = _rabbitMQPassword,
                VirtualHost = _rabbitMQVirtualHost
            };

            SetupNewDayExchange(connectionFactory);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newDayMessage));

            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            channel.BasicPublish(
                exchange: _newDayExchangeName,
                routingKey: string.Empty,
                mandatory: false,
                basicProperties: basicProperties,
                body: body);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Set up 'new day' events fanout exchange with two queues.
        /// </summary>
        private void SetupNewDayExchange(IConnectionFactory connectionFactory)
        {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            ExchangeDeclare(connectionFactory, _newDayExchangeName, "fanout");
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

            QueueDeclareAndBind(
                connectionFactory,
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                _newDayGeneratorQueueName,
                _newDayExchangeName,
                routingKey: string.Empty);

            QueueDeclareAndBind(
                connectionFactory,
                _newDayShipQueueName,
                _newDayExchangeName,
                routingKey: string.Empty);
        }

        /// <summary>
        /// Declare exchange with name <paramref name="exchangeName"/>
        /// and type <paramref name="exchangeType"/>
        /// if such exchange is not declared.
        /// </summary>
        private void ExchangeDeclare(
            IConnectionFactory connectionFactory,
            string exchangeName,
            string exchangeType)
        {
            try
            {
                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.ExchangeDeclarePassive(exchangeName);
            }
            catch (OperationInterruptedException)
            {
                _logger.LogInformation(
                    "Declare exchange {exchangeName} with {exchangeType} type",
                    exchangeName,
                    exchangeType);

                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.ExchangeDeclare(
                    exchange: exchangeName,
                    type: exchangeType,
                    durable: true,
                    autoDelete: false);
            }
        }

        /// <summary>
        /// Declare queue with name <paramref name="queueName"/>
        /// if such queue is not declared
        /// and bind it to exchange <paramref name="exchangeName"/>
        /// with routing key <paramref name="routingKey"/>.
        /// </summary>
        private void QueueDeclareAndBind(
            IConnectionFactory connectionFactory,
            string queueName,
            string exchangeName,
            string routingKey)
        {
            try
            {
                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.QueueDeclarePassive(queueName);
            }
            catch (OperationInterruptedException)
            {
                _logger.LogInformation("Declare queue {queueName}", queueName);

                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);
            }
            finally
            {
                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.QueueBind(
                    queue: queueName,
                    exchange: exchangeName,
                    routingKey: routingKey);
            }
        }
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        #endregion
    }
}
