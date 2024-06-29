using FluentValidation;
using FluentValidation.AspNetCore;
using GameController.API.Mappers;
using GameController.API.Validators.User;
using GameController.Services.Interfaces;
using GameController.Services.Mappers;
using GameController.Services.Services;

namespace SessionController
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
            if (environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseHttpsRedirection();

            application.UseRouting();
            application.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Configure service collection with DI.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(SessionMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(UserMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(RabbitMQMapper)));

            services.AddTransient<IGeneratorService, GeneratorService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShipService, ShipService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRabbitMQService, RabbitMQService>();

            services.AddControllers();

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateUserModelValidator>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: new() { Title = "Game Controller API", Version = "v1" });
                options.EnableAnnotations();
            });
        }

        #endregion
    }
}
