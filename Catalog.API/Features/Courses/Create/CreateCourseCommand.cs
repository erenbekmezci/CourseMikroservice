using CourseMikroservice.Shared;
using MediatR;

namespace Catalog.API.Features.Courses.Create;

    public record CreateCourseCommand(string Name , string Description , string ImageUrl , decimal Price , Guid CategoryId ): IRequest<ServiceResult<Guid>>;


