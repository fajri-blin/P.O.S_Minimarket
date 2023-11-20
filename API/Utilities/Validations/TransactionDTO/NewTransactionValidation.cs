using API.DTOs.TransactionsDTO;
using FluentValidation;

namespace API.Utilities.Validations.TransactionDTO;

public class NewTransactionValidation : AbstractValidator<NewTransactionDTO>
{
    public NewTransactionValidation()
    {
        RuleFor(attr => attr.TransactionDate).NotEmpty().WithMessage("Transaction Date is required");
        RuleFor(attr => attr.TotalAmmount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
