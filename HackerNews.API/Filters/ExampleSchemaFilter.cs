using HackerNews.API.Errors;
using HackerNews.Domain.Dto;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HackerNews.API.Filters
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(NewItemDto))
            {
                schema.Example = new OpenApiObject()
                {
                    ["title"] = new OpenApiString("My YC app: Dropbox - Throw away your USB drive"),
                    ["Link"] = new OpenApiString("http://www.getdropbox.com/u/2/screencast.html"),
                };
            }
            if (context.Type == typeof(Error401))
            {
                schema.Example = new OpenApiObject()
                {
                    ["httpCode"] = new OpenApiInteger(401),
                    ["message"] = new OpenApiString("Requested resource requires authentication"),
                };
            }
            if (context.Type == typeof(Error403))
            {
                schema.Example = new OpenApiObject()
                {
                    ["httpCode"] = new OpenApiInteger(403),
                    ["message"] = new OpenApiString("Authentication successful, but access is denied due to insufficient privileges"),
                };
            }
            if (context.Type == typeof(Error404))
            {
                schema.Example = new OpenApiObject()
                {
                    ["httpCode"] = new OpenApiInteger(404),
                    ["message"] = new OpenApiString("Requested resource was not found"),
                };
            }
            if (context.Type == typeof(Error500))
            {
                schema.Example = new OpenApiObject()
                {
                    ["httpCode"] = new OpenApiInteger(500),
                    ["score"] = new OpenApiString("UnexpectedError"),
                };
            }
        }
    }
}
