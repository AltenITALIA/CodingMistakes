using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodingMistakes.WebApi.Documentation
{
    public class CultureAwareOperationFilter : IOperationFilter
    {
        private readonly IList<IOpenApiAny> supportedLanguages;

        public CultureAwareOperationFilter(IServiceProvider serviceProvider)
        {
            supportedLanguages = serviceProvider.GetService<IOptions<RequestLocalizationOptions>>()?
                .Value?.SupportedCultures?.Select(c => new OpenApiString(c.Name)).Cast<IOpenApiAny>().ToList();
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (supportedLanguages?.Count > 1)
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<OpenApiParameter>();
                }

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = HeaderNames.AcceptLanguage,
                    In = ParameterLocation.Header,
                    Required = false,
                    Schema = new OpenApiSchema { Type = "String", Enum = supportedLanguages, Default = supportedLanguages.FirstOrDefault() }
                });
            }
        }
    }

}
