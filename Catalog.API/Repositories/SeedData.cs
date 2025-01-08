using Catalog.API.Features.Categories;
using Catalog.API.Features.Courses;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public static class SeedData
    {
        public async static Task SeedDataExt(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                   new() {Id = NewId.NextSequentialGuid() , Name = "Development" },
                   new() {Id = NewId.NextSequentialGuid() , Name = "Business" },
                   new() {Id = NewId.NextSequentialGuid() , Name = "IT & Software" },
                   new() {Id = NewId.NextSequentialGuid() , Name = "Office Productivity" },
                   new() {Id = NewId.NextSequentialGuid() , Name = "Personal Development" },
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
            if (!context.Courses.Any())
            {
                var categoryId = await context.Categories.FirstAsync();
                var userId = NewId.NextSequentialGuid();
                var courses = new List<Course>
                {
                    new() 
                    {
                        Id = NewId.NextSequentialGuid(), 
                        Name = "C# Fundamentals",
                        Description = "C# Fundamentals" ,
                        Price = 100, 
                        CategoryId = categoryId.Id,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 10, Rating = 0 , EducatorFullName= "Eren Bekmezci" },
                        UserId = userId

                    },
                    new()
                    {
                        Id = NewId.NextSequentialGuid(),
                        Name = "C# Advanced",
                        Description = "C# Advanced" ,
                        Price = 200,
                        CategoryId = categoryId.Id,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 20, Rating = 0 , EducatorFullName= "Eren Bekmezci" },
                        UserId = userId
                    },
                    new()
                    {
                        Id = NewId.NextSequentialGuid(),
                        Name = "C# Design Patterns",
                        Description = "C# Design Patterns" ,
                        Price = 300,
                        CategoryId = categoryId.Id,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 30, Rating = 0 , EducatorFullName= "Eren Bekmezci" },
                        UserId = userId
                    },
                    new()
                    {
                        Id = NewId.NextSequentialGuid(),
                        Name = "C# Best Practices",
                        Description = "C# Best Practices" ,
                        Price = 400,
                        CategoryId = categoryId.Id,
                        Created = DateTime.UtcNow,
                        Feature = new Feature { Duration = 40, Rating = 0 , EducatorFullName= "Eren Bekmezci" },
                        UserId = userId
                    },

                };
                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();

            }
        }
    }
}
