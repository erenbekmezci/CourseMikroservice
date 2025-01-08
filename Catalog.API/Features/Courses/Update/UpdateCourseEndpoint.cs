using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.GetAll;
using CourseMikroservice.Shared.Extensions;
using CourseMikroservice.Shared.Filters;
using MediatR;

namespace Catalog.API.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {

        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand request, IMediator mediator) => (await mediator.Send(request)).ToGenericResult())
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>()
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}