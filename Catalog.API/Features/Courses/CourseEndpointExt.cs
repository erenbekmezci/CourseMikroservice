using Catalog.API.Features.Categories.Create;
using Catalog.API.Features.Courses.Create;

namespace Catalog.API.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseEndpointExt(this WebApplication app)
        {
            app.MapGroup("/api/courses")
                .CreateCourseGroupItemEndpoint().WithTags("Courses");


        }
    }
}
