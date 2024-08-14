using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace web_api_google_authentication.Configurations
{
    internal class SwaggerConfigurationOptions
   : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfigurationOptions(
          IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  description.GroupName,
                  CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
          ApiVersionDescription description)
        {
            var assemblyDescription =
              typeof(Program).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            var info = new OpenApiInfo
            {
                Title = $"MTN Adapter API V{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated
                ? $"{assemblyDescription} - DEPRECATED"
                : $"{assemblyDescription}"
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
