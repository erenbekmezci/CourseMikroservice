using CourseMikroservice.Shared;
using MediatR;

namespace Catalog.API.Features.Categories.Create;

//CQRS Command pattern için oluşturulmuş bir sınıf
//COMMAND -> create update delete işlemleri için kullanılır
//Command pattern, bir isteği bir nesne olarak temsil etmek için kullanılır

public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;


