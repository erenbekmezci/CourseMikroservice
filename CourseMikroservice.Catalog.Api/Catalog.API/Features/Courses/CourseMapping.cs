using AutoMapper;
using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.Dtos;

namespace Catalog.API.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();




        }
    }
}
