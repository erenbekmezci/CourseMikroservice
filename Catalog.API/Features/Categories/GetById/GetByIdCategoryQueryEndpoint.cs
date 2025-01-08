using AutoMapper;
using Catalog.API.Features.Categories.Dtos;
using Catalog.API.Repositories;
using CourseMikroservice.Shared;
using CourseMikroservice.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Features.Categories.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;
    public class GetByIdCategoryQueryHandler(AppDbContext _context , IMapper _mapper) : IRequestHandler<GetByIdCategoryQuery, ServiceResult<CategoryDto>>
    {
       
        public async Task<ServiceResult<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Error(title: "Category not found", statusCode: HttpStatusCode.NotFound);
            }

            var mappedCategory = _mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.SuccessAsOk(mappedCategory);
        }
    }
    public static class GetByIdCategoryQueryEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetByIdCategoryQuery(id));
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0);

            return group;
        }
    }
}
