using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace LeadLeague.OpenApi
{
    internal sealed class BearerSecuritySchemeTransformer() : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var requirements = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header
                }
            };
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = requirements;

            return Task.CompletedTask;
        }
    }
}
