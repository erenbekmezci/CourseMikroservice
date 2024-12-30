using Catalog.API.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace Catalog.API.Repositories
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //collection/document/field(sütün)

            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name");
            builder.Property(x => x.Description).HasElementName("description");
            builder.Property(x => x.ImagePath).HasElementName("imagePath");
            builder.Property(x => x.Price).HasElementName("price");
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.Created).HasElementName("created");
            builder.Ignore(x => x.Category);

            builder.OwnsOne(x => x.Feature, feature =>
            {
                feature.Property(x => x.Duration).HasElementName("duration");
                feature.Property(x => x.Rating).HasElementName("rating");
                feature.Property(x => x.EducatorFullName).HasElementName("educatorFullName");
            });
        }
    }
}
