using GameController.Services.Interfaces;
using GameController.Services.Mappers;
using GameController.Services.Services;
using GameController.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace GameController.Services
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> for services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region public methods

        /// <summary>
        /// Configure project-specific service collection with DI.
        /// </summary>
        public static IServiceCollection ConfigureApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(RabbitMQMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(SessionMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(UserMapper)));

            IConfigurationSection rabbitMQSection = configuration.GetSection("RabbitMQ");
            services.Configure<RabbitMQSettings>(rabbitMQSection.Bind);
            SetupRabbitMQ(
                rabbitMQSection.Get<RabbitMQSettings>() ??
                throw new Exception("RabbitMQ settings are required"));

            services.Configure<SpaceShipApiSettings>(x => configuration.GetSection("SpaceShipApi").Bind(x));

            services.AddTransient<IGeneratorService, GeneratorService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShipService, ShipService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRabbitMQService, RabbitMQService>();

            return services;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Set up 'new day' events fanout exchange with two queues.
        /// </summary>
        private static void SetupRabbitMQ(RabbitMQSettings rabbitMQSettings)
        {
            ConnectionFactory connectionFactory = new()
            {
                HostName = rabbitMQSettings.Hostname,
                Port = rabbitMQSettings.Port,
                UserName = rabbitMQSettings.Username,
                Password = rabbitMQSettings.Password,
                VirtualHost = rabbitMQSettings.VirtualHost
            };

            ExchangeDeclare(connectionFactory, rabbitMQSettings.NewDayExchangeName, "fanout");

            QueueDeclareAndBind(
                connectionFactory,
                rabbitMQSettings.NewDayQueueNameGenerator,
                rabbitMQSettings.NewDayExchangeName,
                routingKey: string.Empty);

            QueueDeclareAndBind(
                connectionFactory,
                rabbitMQSettings.NewDayQueueNameShip,
                rabbitMQSettings.NewDayExchangeName,
                routingKey: string.Empty);
        }

        /// <summary>
        /// Declare exchange with name <paramref name="exchangeName"/>
        /// and type <paramref name="exchangeType"/>
        /// if such exchange is not declared.
        /// </summary>
        private static void ExchangeDeclare(
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
        private static void QueueDeclareAndBind(
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

        #endregion
    }
}
