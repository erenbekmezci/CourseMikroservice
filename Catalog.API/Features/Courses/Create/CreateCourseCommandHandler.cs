using AutoMapper;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;

namespace Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper , IIdentityService identityService) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId,cancellationToken);

            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found.",$"The Category with id({request.CategoryId}) " +
                    $"was not found", System.Net.HttpStatusCode.NotFound);
            }




            var courseExists = await context.Courses.AnyAsync(x => x.Name == request.Name , cancellationToken);

            if (courseExists)
            {
                return ServiceResult<Guid>.Error("Course already exists.", $"The Course with name({request.Name}) " +
                    $"already exists", System.Net.HttpStatusCode.BadRequest);
            }

            var mappedCourse = mapper.Map<Course>(request);
            mappedCourse.Id = NewId.NextSequentialGuid();
            mappedCourse.UserId = identityService.GetUserId;

            mappedCourse.Created = DateTime.Now;
            mappedCourse.Feature = new Feature
            {
                Rating =0,
                Duration = 10,
                EducatorFullName = "John Doe",
            };

            await context.Courses.AddAsync(mappedCourse , cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(mappedCourse.Id, $"api/courses/{mappedCourse.Id}");


        }
    }
}
