using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GameController.API.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region private constants

        private const string BearerInputDescription =
            "Enter the Bearer Token as follows:\n\n" +
            "`Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`\n\n" +
            "Be sure to include `Bearer` followed by a space, then your token.";

        #endregion

        #region public methods

        /// <summary>
        /// Configure swagger for services:<br/>
        /// * configure documentation;<br/>
        /// * enable annotations;<br/>
        /// * set up security.
        /// </summary>
        public static IServiceCollection ConfigureSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                ConfigureSwaggerDoc(options);

                options.EnableAnnotations();

                AddSwaggerSecurity(options);
            });

            return services;
        }

        #endregion

        #region private methods

        private static void ConfigureSwaggerDoc(SwaggerGenOptions options)
        {
            options.SwaggerDoc(
                name: "v1",
                info: new OpenApiInfo
                {
                    Title = "Game Controller API",
                    Version = "v1"
                });
        }

        private static void AddSwaggerSecurity(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(
                name: "Bearer",
                securityScheme: CreateSecurityScheme());

            options.AddSecurityRequirement(CreateSecurityRequirement());
        }

        private static OpenApiSecurityScheme CreateSecurityScheme() => new()
        {
            Name = "Authorization",
            Description = BearerInputDescription,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        };

        private static OpenApiSecurityRequirement CreateSecurityRequirement() => new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        };

        #endregion
    }
}
