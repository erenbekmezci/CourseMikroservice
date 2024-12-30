using Catalog.API.Options;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddRepositoryExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoOptions = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(mongoOptions.ConnectionString);
            });

            services.AddScoped<AppDbContext>(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var mongoOptions = sp.GetRequiredService<MongoOptions>();
                var database = mongoClient.GetDatabase(mongoOptions.Database);

                return AppDbContext.Create(database);

            });

            return services;
        }
    }
}
