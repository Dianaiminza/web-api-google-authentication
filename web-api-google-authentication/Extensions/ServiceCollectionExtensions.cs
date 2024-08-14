using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using web_api_google_authentication.Configurations;

namespace web_api_google_authentication.Extensions;

public static class ServiceCollectionExtensions
{
    

    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();
    }

    

    public static void ConfigureLoggingProviders(this IServiceCollection services)
    {
        // https://weblog.west-wind.com/posts/2018/Dec/31/Dont-let-ASPNET-Core-Default-Console-Logging-Slow-your-App-down
        services.AddLogging(config =>
        {
            // clear out default configuration. Providers will be added by serilog
            config.ClearProviders();
        });
    }
}
