using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Features.Categories.Create
{
    //commandları handle eden sınıf
    //business burda yazılır
    //efcore dbcontext repository ve uniofworkpattern design patternları zaten implente eder tabi dappera geçmiceksen
    //amaç repository ve unitofwork patternlarından kurtulmak ekstra bir katman oluşturmamak
    //ama yarın birgün dapper geçmek isterseniz bu katmanı eklmeniz gerekecek
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            
            var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name);

            if (existCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error(title: "Category name already exists ", description: $"Category name {request.Name} already exists",HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Id = NewId.NextSequentialGuid(),
                Name = request.Name
            };  

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var response = new CreateCategoryResponse(Id: category.Id);
           
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(response , "<empty>");

        }
    }
}
