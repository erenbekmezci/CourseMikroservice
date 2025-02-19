using AutoMapper;
using Catalog.API.Features.Categories.Dtos;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Features.Categories.GetAll
{
    public record GetAllCategoryQuery() : IRequest<ServiceResult<List<CategoryDto>>>;
    public class GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        

        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            var mappedCategories =  mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(mappedCategories);
        }

       
    }


    public static class GetAllCategoryQueryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 2);

            return group;
        }
    }
}
