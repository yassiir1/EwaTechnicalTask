using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumValues = Enum.GetNames(context.Type);
            var openApiEnumValues = enumValues.Select(name => new OpenApiString(name)).ToList<IOpenApiAny>();
            schema.Enum = openApiEnumValues;
        }
    }
}
