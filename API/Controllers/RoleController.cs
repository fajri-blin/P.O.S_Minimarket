using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult GetRole()
    {
        var listRole = _roleService.GetAll();
        if (listRole == null)
        {
            return NotFound();
        }
        return Ok(listRole);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Guid guid)
    {
        var delete = _roleService.Delete(guid);
        switch(delete)
        {
            case -1:
                return NotFound();
            case 0:
                return BadRequest();
        }
        return Ok();
    }
}
