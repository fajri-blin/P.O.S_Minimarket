using API.DTOs.EmployeesDTO;
using API.DTOs.ProductDTO;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/")]
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
        if (listEmployee == null) return NotFound(
            new ResponseHandlers<EmployeeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });

        return Ok(new ResponseHandlers<IEnumerable<EmployeeDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listEmployee,
        });
    }

    [HttpPost("AddEmployee/")]
    public IActionResult AddEmployee(NewEmployeeDTO newEmployeeDTO)
    {
        var created = _employeeService.Create(newEmployeeDTO);
        if (created != null)
        {
            return Ok(new ResponseHandlers<EmployeeDTO>
            {
                Code= StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully Added Data",
                Data = created,
            });
        }
        return BadRequest(new ResponseHandlers<EmployeeDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Data Failed to Add",
            Data = created
        });
    }

    [HttpDelete("Delete/{guid}")]
    public IActionResult DeleteEmployee(Guid guid) 
    { 
        var delete = _employeeService.Delete(guid);
        switch(delete)
        {
            case -1:
                return NotFound(new ResponseHandlers<EmployeeDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data that want to delete is not found"
                });
            case 0:
                return BadRequest(new ResponseHandlers<EmployeeDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Delete"
                });
        }
        return Ok(new ResponseHandlers<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
            Data = delete
        });
    }
}
