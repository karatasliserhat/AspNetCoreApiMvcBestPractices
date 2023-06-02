using FluentValidation;
using NlayerApi.Core.DTOs;

namespace NlayerApi.Service.Validations
{
    public class ProductValidation : AbstractValidator<ProductCreateDto>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} null olamaz").NotNull().WithMessage("{PropertyName} null olamaz");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} geçerli bir değer girin");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} geçerli bir değer girin");
            RuleFor(x => x.Priace).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} geçerli bir değer girin");
        }
    }
}
