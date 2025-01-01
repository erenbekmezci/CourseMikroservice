using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Categories.GetAll;
using Catalog.API.Features.Categories.GetById;
using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

namespace Catalog.API.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .WithTags("Categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();
        }
    }
}
