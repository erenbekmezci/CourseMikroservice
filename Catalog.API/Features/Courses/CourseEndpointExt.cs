using Asp.Versioning.Builder;
using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Categories.GetAll;
using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.Delete;
using Catalog.API.Features.Courses.GetAll;
using Catalog.API.Features.Courses.GetAllByUserId;
using Catalog.API.Features.Courses.GetById;
using Catalog.API.Features.Courses.Update;

namespace Catalog.API.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseEndpointExt(this WebApplication app , ApiVersionSet apiVersionSet)
        {
            app.MapGroup("/api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint()
                .GetByIdCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetAllCoursesByUserIdGroupItem();


        }
    }
}
