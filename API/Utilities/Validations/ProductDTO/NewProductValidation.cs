using API.Contracts.Repositories.Entities;
using API.DTOs.ProductDTO;
using FluentValidation;

namespace API.Utilities.Validations.ProductDTO;

public class NewProductValidation : AbstractValidator<NewProductDTO>
{
    private readonly IProductRepository _productRepository;

    public NewProductValidation(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(attr => attr.BarcodeID)
            .NotEmpty().WithMessage("BarcodeID is required")
            .Must(UniqueBarcode).WithMessage("Product was already registered");

        RuleFor(attr => attr.Title)
            .NotEmpty().WithMessage("Title is required");
    }

    public bool UniqueBarcode(string property)
    {
        return !_productRepository.UniqueBarcode(property);
    }
}
