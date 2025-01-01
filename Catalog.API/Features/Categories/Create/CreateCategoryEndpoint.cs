using CourseMikroservice.Shared.Extensions;
using CourseMikroservice.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command ,IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();

            }).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
