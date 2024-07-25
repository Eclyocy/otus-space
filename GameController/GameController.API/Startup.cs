﻿using FluentValidation;
using FluentValidation.AspNetCore;
using GameController.API.Mappers;
using GameController.API.ServicesExtensions;
using GameController.API.Validators.User;
using GameController.Database;
using GameController.Services;
using GameController.Services.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace GameController
{
    /// <summary>
    /// Main start up class.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        #region public properties

        /// <summary>
        /// Application configuration instance.
        /// </summary>
        public IConfiguration Configuration { get; } = configuration;

        /// <summary>
        /// Web-hosting environment instance.
        /// </summary>
        public IWebHostEnvironment Environment { get; } = environment;

        #endregion

        #region public methods

        /// <summary>
        /// Configure HTTP request pipeline.
        /// </summary>
        public void Configure(
            IApplicationBuilder application,
            IWebHostEnvironment environment)
        {
            application.UseExceptionHandler(x => x.UseCustomExceptionHandler());

            application.UseSwagger();
            application.UseSwaggerUI();

            application.UseHttpsRedirection();

            application.UseRouting();
            application.UseCors();
            application.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SetupRabbitMQ(application.ApplicationServices.GetService<IOptions<RabbitMQSettings>>());
        }

        /// <summary>
        /// Configure application service collection with DI.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase();

            services.ConfigureApplicationServices(Configuration);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddAutoMapper(x => x.AddProfile(typeof(SessionMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(UserMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(ShipMapper)));

            services.AddControllers();

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: new() { Title = "Game Controller API", Version = "v1" });
                options.EnableAnnotations();
            });
        }

        #endregion

        #region private methods

        /// <summary>
        /// Set up 'new day' events fanout exchange with two queues.
        /// </summary>
        private static void SetupRabbitMQ(IOptions<RabbitMQSettings> rabbitMQOptions)
        {
            RabbitMQSettings rabbitMQSettings = rabbitMQOptions.Value;

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
