using API.DTOs.EmployeesDTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult GetEmployee()
    {
        var listEmployee = _employeeService.Get();
        if (listEmployee == null) return NotFound();

        return Ok(listEmployee);
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

    [HttpDelete("Delete")]
    public IActionResult DeleteEmployee(Guid guid) 
    { 
        var delete = _employeeService.Delete(guid);
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
