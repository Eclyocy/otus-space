using FluentValidation;
using FluentValidation.AspNetCore;
using GameController.API.Mappers;
using GameController.API.ServicesExtensions;
using GameController.API.Validators.User;
using GameController.Database;
using GameController.Services;

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

            if (true || environment.IsDevelopment())
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
        /// Configure application service collection with DI.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase();

            services.ConfigureApplicationServices(Configuration);

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
    }
}
