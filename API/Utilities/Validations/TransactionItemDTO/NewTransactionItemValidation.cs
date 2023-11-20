using API.DTOs.TransactionsItemDTO;
using FluentValidation;

namespace API.Utilities.Validations.TransactionItemDTO;

public class NewTransactionItemValidation : AbstractValidator<NewTransactionItemDTO>
{
    public NewTransactionItemValidation()
    {
        RuleFor(attr => attr.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must greater than or equal to 0");

        RuleFor(attr => attr.Subtotal)
            .NotEmpty().WithMessage("Subtitle is required")
            .GreaterThanOrEqualTo(0).WithMessage("Subtotal must greater than or equal to 0");
    }
}
