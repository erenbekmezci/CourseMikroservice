﻿using Catalog.API.Features.Categories.Dtos;

namespace Catalog.API.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Name, string Description, decimal Price,string ImageUrl, CategoryDto Category,FeatureDto Feature, Guid UserId);
    
}
