using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Configs;

public class AddAcceptLanguageHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var languages = Enum
            .GetValues(typeof(Languages))
            .Cast<Languages>()
            .Select(x => new OpenApiString(x.ToString()));

        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Enum = languages.Cast<IOpenApiAny>().ToList()
            }
        });
    }
}
