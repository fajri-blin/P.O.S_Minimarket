using API.DTOs.RolesDTO;
using FluentValidation;

namespace API.Utilities.Validations.RoleDto;

public class NewRoleValidation : AbstractValidator<NewRoleDTO>
{
    public NewRoleValidation()
    {
        RuleFor(attr => attr.Name)
            .NotEmpty().WithMessage("Name is Required");
    }
}
