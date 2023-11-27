using API.DTOs.TransactionsDTO;
using API.Utilities.Handlers;
using FluentValidation;

namespace API.Utilities.Validations.TransactionDTO;

public class NewTransactionValidation : AbstractValidator<NewTransactionDTO>
{
    public NewTransactionValidation()
    {
        RuleFor(attr => attr.TransactionDate).NotEmpty().WithMessage("Transaction Date is required");
        RuleFor(attr => attr.TotalAmmount)
        .NotEmpty().WithMessage("TotalAmmount is required")
        .GreaterThanOrEqualTo(0)
        .Must(OnlyNumberHandler<decimal>.ValidNumber).WithMessage("TotalAmount should contain only numbers, dots, or commas");
    }
}
