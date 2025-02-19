using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.GetById;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using CourseMikroservice.Shared.Filters;
using MediatR;

namespace Catalog.API.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id) : IRequest<ServiceResult>;

    public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.Id);
            if (course == null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            context.Courses.Remove(course);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
    public static class DeleteCourseCommandEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .MapToApiVersion(1, 0);


            return group;
        }
    }
}
