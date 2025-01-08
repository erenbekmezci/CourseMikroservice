using CourseMikroservice.Shared;
using MediatR;

namespace Catalog.API.Features.Courses.Update;

public record UpdateCourseCommand(Guid Id, string Name, string Description, decimal Price,string? ImageUrl, Guid CategoryId) : IRequest<ServiceResult>;


