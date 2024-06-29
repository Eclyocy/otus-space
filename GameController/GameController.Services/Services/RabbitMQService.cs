using GameController.Services.Interfaces;
using GameController.Services.Models.Message;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with Rabbit MQ.
    /// </summary>
    public class RabbitMQService : IRabbitMQService
    {
        #region private constants

        private const string NewDayExchangeName = "x_new_day";
        private const string NewDayExchangeType = "fanout";

        #endregion

        #region private fields

        private readonly string _rabbitMQHostname;
        private readonly int _rabbitMQPort;
        private readonly string _rabbitMQUsername;
        private readonly string _rabbitMQPassword;

        private readonly string _newDayGeneratorQueueName;
        private readonly string _newDayShipQueueName;

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
            _rabbitMQPort = int.Parse(configuration["RABBITMQ_PORT"]);
            _rabbitMQUsername = configuration["RABBITMQ_USERNAME"];
            _rabbitMQPassword = configuration["RABBITMQ_PASSWORD"];

            _newDayGeneratorQueueName = configuration["QUEUENAME_NEWDAY_GENERATOR"];
            _newDayShipQueueName = configuration["QUEUENAME_NEWDAY_SHIP"];
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public void SendNewDayMessage(NewDayMessage newDayMessage)
        {
            _logger.LogInformation("Send 'new day' message to Rabbit MQ {message}", newDayMessage);

            ConnectionFactory connectionFactory = new()
            {
                HostName = _rabbitMQHostname,
                Port = _rabbitMQPort,
                UserName = _rabbitMQUsername,
                Password = _rabbitMQPassword
            };

            SetupExchange(connectionFactory);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newDayMessage));

            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.BasicPublish(
                exchange: NewDayExchangeName,
                routingKey: string.Empty,
                mandatory: false,
                basicProperties: null,
                body: body);
        }

        #endregion

        #region private methods

        private void SetupExchange(IConnectionFactory connectionFactory)
        {
            ExchangeDeclare(connectionFactory, NewDayExchangeName);

            QueueDeclareAndBind(connectionFactory, _newDayGeneratorQueueName, NewDayExchangeName);
            QueueDeclareAndBind(connectionFactory, _newDayShipQueueName, NewDayExchangeName);
        }

        private void ExchangeDeclare(
            IConnectionFactory connectionFactory,
            string exchangeName)
        {
            try
            {
                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.ExchangeDeclarePassive(NewDayExchangeName);
            }
            catch (OperationInterruptedException)
            {
                _logger.LogInformation(
                    "Declare exchange {exchangeName} with {exchangeType} type",
                    NewDayExchangeName,
                    NewDayExchangeType);

                using IConnection connection = connectionFactory.CreateConnection();
                using IModel channel = connection.CreateModel();

                channel.ExchangeDeclare(
                    exchange: NewDayExchangeName,
                    type: NewDayExchangeType,
                    durable: true,
                    autoDelete: false);
            }
        }

        private void QueueDeclareAndBind(
            IConnectionFactory connectionFactory,
            string queueName,
            string exchangeName)
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
                    routingKey: string.Empty);
            }
        }

        #endregion
    }
}
