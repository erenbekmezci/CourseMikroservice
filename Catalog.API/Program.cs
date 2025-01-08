using Catalog.API;
using Catalog.API.Features.Categories;
using Catalog.API.Features.Courses;
using Catalog.API.Options;
using Catalog.API.Repositories;
using CourseMikroservice.Shared.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddRepositoryExt();


builder.Services.AddVersionExt();//Add Versioning

builder.Services.AddCommonServiceExt(typeof(CatelogAssembly));


var app = builder.Build();
app.SeedDataExt().ContinueWith(x =>
{
    if (x.IsFaulted)
    {
        Console.WriteLine(x.Exception.Message);
    }
    else
    {
        Console.WriteLine("Seed Data Completed");
    }


});

app.AddCategoryGroupExt(app.AddVersionSetExt());
app.AddCourseEndpointExt(app.AddVersionSetExt());



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();


