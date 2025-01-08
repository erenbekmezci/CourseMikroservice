using Asp.Versioning.Builder;
using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Categories.GetAll;
using Catalog.API.Features.Categories.GetById;
using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

namespace Catalog.API.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupExt(this WebApplication app , ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();
        }
    }
}
