using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace ServiceBase.API
{
    public static partial class StartupExtensions
    {
        public static IServiceCollection EnableSwagger(this IServiceCollection services, string serviceName)
        {
            services.AddOpenApiDocument(options =>
            {
                options.SchemaType = SchemaType.OpenApi3;
                options.Title = serviceName;

                options.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Flow = OpenApiOAuth2Flow.Application,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    TokenUrl = "TBA",
                });

                options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));

            });
            return services;
        }

        public static IApplicationBuilder EnableSwagger(this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider,
            string servicePath, string serviceName)
        {
            app.UseOpenApi(options =>
            {
                options.Path = "/swagger/{documentName}/swagger.json";
                options.PostProcess = (document, request) =>
                {
                    document.Schemes.Clear();
                    document.Schemes.Add(OpenApiSchema.Https);
                };
            });
            app.UseSwaggerUi3(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerRoutes.Add(
                        new SwaggerUi3Route(
                            $"{description.GroupName.ToUpperInvariant()}",
                            string.IsNullOrWhiteSpace(servicePath)
                                ? $"/swagger/{description.GroupName}/swagger.json"
                                : $"/swagger/{servicePath}/{description.GroupName}/swagger.json"));
                }

                options.OAuth2Client = new OAuth2ClientSettings
                {
                    AppName = serviceName
                };

                
            });

            return app;
        }
    }
}
