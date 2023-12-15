using API.Contracts.Repositories.Entities;
using API.DTOs.EmployeesDTO;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeDTO;

public class NewEmployeeValidations : AbstractValidator<NewEmployeeDTO>
{
    private readonly IEmployeeRepository _employeeRepository;
    public NewEmployeeValidations(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(employee => employee.Firstname)
            .NotEmpty().WithMessage("FirstName is required")
            .Matches("^[^0-9]+$").WithMessage("Firstname should not contain numbers");

        RuleFor(employee => employee.Lastname)
            .Matches("^[^0-9]+$").WithMessage("Lastname should not contain numbers");

        RuleFor(employee => employee.Username)
            .NotEmpty().WithMessage("Username is required")
            .Must(UniqueProperty).WithMessage("The Account already registered");

        RuleFor(employee => employee.Password)
          .NotEmpty().WithMessage("Password is required.")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
          .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
          .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
          .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
          .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(employee => employee.RoleName)
            .NotEmpty().WithMessage("Role Name is required");
    }

    private bool UniqueProperty(string property)
    {
        return !_employeeRepository.IsUniqueUsername(property);
    }
}
