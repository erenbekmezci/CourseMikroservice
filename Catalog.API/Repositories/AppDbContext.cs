﻿using Catalog.API.Features.Categories;
using Catalog.API.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace Catalog.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                    database.DatabaseNamespace.DatabaseName);


            return new AppDbContext(optionsBuilder.Options);
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
