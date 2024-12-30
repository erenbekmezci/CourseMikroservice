using Catalog.API;
using Catalog.API.Features.Categories;
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

builder.Services.AddCommonServiceExt(typeof(CatelogAssembly));


var app = builder.Build();

app.AddCategoryGroupExt();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();


