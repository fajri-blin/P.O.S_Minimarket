using API.DTOs.TransactionsItemDTO;
using API.Utilities.Handler;
using API.Utilities.Handlers;
using FluentValidation;
using API.Repository.Entities;
using API.Contracts.Repositories;
using API.Contracts.Repositories.Entities;

namespace API.Utilities.Validations.TransactionItemDTO;

public class NewTransactionItemValidation : AbstractValidator<NewTransactionItemDTO>
{
    private readonly IProductRepository _repository;
    public NewTransactionItemValidation(IProductRepository repository)
    {
        _repository = repository;

        RuleFor(attr => attr.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must greater than or equal to 0")
            .Must(OnlyNumberHandler<float>.ValidNumber).WithMessage("Quantity should contain only numbers, dots, or commas");

        RuleFor(attr => attr.Subtotal)
            .NotEmpty().WithMessage("Subtotal is required")
            .GreaterThanOrEqualTo(0).WithMessage("Subtotal must greater than or equal to 0")
            .Must(OnlyNumberHandler<decimal>.ValidNumber).WithMessage("Subtotal should contain only numbers, dots, or commas");
        RuleFor(attr => attr.PriceGuid)
            .NotEmpty().WithMessage("Product guid tidak boleh kosong")
            .Must(guid => guid.HasValue && _repository.IsProductExist(guid.Value)).WithMessage("Product Tidak Tersedia di Database");
    }
}
