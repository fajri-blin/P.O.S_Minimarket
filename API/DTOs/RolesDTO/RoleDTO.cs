using API.Model.Entities;

namespace API.DTOs.RolesDTO;

public class RoleDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static explicit operator Role(RoleDTO dto)
    {
        return new Role
        {
            Guid = dto.Guid,
            Name = dto.Name,
        };
    }

    public static explicit operator RoleDTO(Role role)
    {
        return new RoleDTO
        {
            Guid = role.Guid,
            Name = role.Name,
        };
    }
}
