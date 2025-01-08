using CourseMikroservice.Shared.Extensions;
using CourseMikroservice.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
       public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
       {
            group.MapPost("/", async (CreateCourseCommand request, IMediator mediator) => (await mediator.Send(request)).ToGenericResult())
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>()
                .MapToApiVersion(1, 0); 

            return group;
        }
    }
}
