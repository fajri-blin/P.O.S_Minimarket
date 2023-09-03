using API.Contract.Entities;
using API.Data;
using API.DTOs.RolesDTO;

namespace API.Services;

public class RoleService
{
    private readonly IRoleRepository _roleService;
    private readonly PosDbContext _context;

    public RoleService(RoleService roleService, PosDbContext context)
    {
        _roleService = roleService;
        _context = context;
    }

    public IEnumerable<RoleDTO> GetAll()
    {
        var list = _roleService.GetAll();
        if(list == null || !list.Any()) 
        {
            return null;
        }

        var listDto = list.Select(role => (RoleDTO)role);
        return listDto;
    }

    public int Delete(Guid guid)
    {
        var roleEntity = _roleService.GetByGuid(guid);
        if (roleEntity == null) return -1;

        var delete = _roleService.Delete(roleEntity);
        if(delete == false) return 0;
        return 1;
    }
}
