using AutoMapper;
using Catalog.API.Features.Courses.Dtos;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace Catalog.API.Features.Courses.GetById
{
    public record GetByIdCourseQuery(Guid Id) : IRequest<ServiceResult<CourseDto>>;
    public class GetByIdCourseQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetByIdCourseQuery, ServiceResult<CourseDto>>
    {
   
        public async Task<ServiceResult<CourseDto>> Handle(GetByIdCourseQuery query, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (course == null)
            {
                return ServiceResult<CourseDto>.Error("Course not found",$"Course with id({query.Id}) not found",HttpStatusCode.NotFound);
            }
            var category = await context.Categories.FindAsync(course.CategoryId, cancellationToken);
            course.Category = category!;
            var mappedCourse = mapper.Map<CourseDto>(course);
            return ServiceResult<CourseDto>.SuccessAsOk(mappedCourse);

        }
    }
    public static class GetByIdCourseQueryEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetByIdCourseQuery(id))).ToGenericResult())
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
