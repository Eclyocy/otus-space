using System.Text.Json.Serialization;
using EventGenerator.API.Mappers;
using EventGenerator.API.ServicesExtensions;
using EventGenerator.Database;
using EventGenerator.Services;
using FluentValidation.AspNetCore;

namespace EventGenerator
{
    /// <summary>
    /// Main start up class.
    /// </summary>
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

            services.AddAutoMapper(x => x.AddProfile(typeof(GeneratorMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(EventMapper)));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: new() { Title = "Event Generator Controller API", Version = "v1" });
                options.EnableAnnotations();
            });
        }

        #endregion
    }
}
