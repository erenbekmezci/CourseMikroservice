using FluentValidation;

namespace Catalog.API.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 50).WithMessage("Name must be between 3 and 50 characters");



        }

    }
}
