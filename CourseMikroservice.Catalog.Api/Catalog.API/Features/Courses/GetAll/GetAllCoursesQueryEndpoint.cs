using AutoMapper;
using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.Dtos;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using CourseMikroservice.Shared.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Features.Courses.GetAll
{
    public record GetAllCoursesQuery() : IRequest<ServiceResult<List<CourseDto>>>;

    public class GetAllCoursesQueryHandler(AppDbContext context , IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
    {
       
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
           
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                //her kursun bir kategorisi olmak zorunda o yüzden first kullanabiliriz
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }
            var mappedCourses = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(mappedCourses);


        }
    }
    public static class GetAllCoursesQueryEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
