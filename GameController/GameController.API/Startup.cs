using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GameController.API.Mappers;
using GameController.API.ServicesExtensions;
using GameController.API.Validators.User;
using GameController.Database;
using GameController.Services;
using GameController.Services.Services;
using GameController.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(x => x.UseCustomExceptionHandler());

            // Настройка Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            // Аутентификация должна идти перед авторизацией
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(); // Включение CORS

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SetupRabbitMQ(app.ApplicationServices.GetService<IOptions<RabbitMQSettings>>());
        }

        /// <summary>
        /// Configure application service collection with DI.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация базы данных
            services.ConfigureDatabase();

            // Регистрация сервисов приложения
            services.ConfigureApplicationServices(Configuration);

            // Настройка CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Настройка AutoMapper для различных мапперов
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SessionMapper>();
                cfg.AddProfile<UserMapper>();
                cfg.AddProfile<ShipMapper>();
            });

            // Добавление контроллеров
            services.AddControllers();

            // Настройка FluentValidation
            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

            // Настройка Swagger с аннотациями
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Game Controller API", Version = "v1" });
                options.EnableAnnotations();
            });

            // Проверка наличия ключа JWT
            var jwtKey = Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("JWT key is not configured.");
            }

            // Настройка JWT авторизации
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            // Регистрация сервиса для работы с JWT
            services.AddScoped<JwtService>();
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
