using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EWATask.Helper
{
    public class UnauthorizedResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterDescriptors = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var authorizeData = filterDescriptors.Select(filterInfo => filterInfo.Filter)
                .OfType<AuthorizeFilter>()
                .FirstOrDefault();

            if (authorizeData != null)
            {
                var policy = authorizeData.Policy;
                if (policy != null && policy.AuthenticationSchemes != null && policy.AuthenticationSchemes.Any(s => s == "Cookie"))
                {
                    operation.Responses.Remove("404");
                    operation.Responses.Add("401", new OpenApiResponse
                    {
                        Description = "Unauthorized",
                    });
                    if (policy.Requirements.Any(r => r is RolesAuthorizationRequirement))
                    {
                        operation.Responses.Add("403", new OpenApiResponse
                        {
                            Description = "You must be a Normal User.",
                        });
                    }
                }
            }
        }
    }
}
