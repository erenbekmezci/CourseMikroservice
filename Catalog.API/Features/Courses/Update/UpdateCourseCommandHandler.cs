using AutoMapper;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context , IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (course == null)
            {
                return ServiceResult.Error("Course not found", $"Course with id({request.Id}) not found", HttpStatusCode.NotFound);
            }

            course.Name = request.Name;
            course.Description = request.Description;
            course.Price = request.Price;
            course.ImageUrl = request.ImageUrl;
            course.CategoryId = request.CategoryId;
            
            context.Update(course);

            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }
    }
}
