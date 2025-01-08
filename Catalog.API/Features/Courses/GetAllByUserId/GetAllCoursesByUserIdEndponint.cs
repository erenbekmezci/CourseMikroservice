using AutoMapper;
using Catalog.API.Features.Courses.Dtos;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.GridFS;

namespace Catalog.API.Features.Courses.GetAllByUserId;


    public record GetAllCoursesByUserIdQuery(Guid UserId) : IRequest<ServiceResult<List<CourseDto>>>;
    
    public class GetAllCoursesByUserIdHandler(AppDbContext context , IMapper mapper) : IRequestHandler<GetAllCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
    {      

        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken: cancellationToken);
            
            var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);
            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            return ServiceResult<List<CourseDto>>.SuccessAsOk(mapper.Map<List<CourseDto>>(courses));
        }
    }

    public static class GetAllCoursesByUserIdEndponint
    {
        public static RouteGroupBuilder GetAllCoursesByUserIdGroupItem(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) => (await mediator.Send(new GetAllCoursesByUserIdQuery(userId))).ToGenericResult())
            .MapToApiVersion(1, 0);

        return group;


        } 
        
    }

