using API.DTOs.EmployeesDTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[contoller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("AddEmployee")]
    public IActionResult AddEmployee(NewEmployeeDTO newEmployeeDTO)
    {
        var created = _employeeService.Create(newEmployeeDTO);
        if (created != null)
        {
            return Ok(created);
        }
        return NotFound();
    }

}
