﻿using API.DTOs.EmployeesDTO;
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
            new ResponseHandler<EmployeeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });

        return Ok(new ResponseHandler<IEnumerable<EmployeeDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listEmployee,
        });
    }

    [HttpGet("{guid}/")]
    public IActionResult GetEmployee(Guid guid)
    {
        var employee = _employeeService.Get(guid);
        if (employee == null) return NotFound(
            new ResponseHandler<EmployeeDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        return Ok(new ResponseHandler<EmployeeDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data was founded",
            Data = employee,
        });
    }

    [HttpPost("AddEmployee/")]
    public IActionResult AddEmployee(NewEmployeeDTO newEmployeeDTO)
    {
        var created = _employeeService.Create(newEmployeeDTO);
        if (created != null)
        {
            return Ok(new ResponseHandler<EmployeeDTO>
            {
                Code= StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully Added Data",
                Data = created,
            });
        }
        return BadRequest(new ResponseHandler<EmployeeDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Data Failed to Add",
            Data = created
        });
    }

    [HttpPut("UpdatePrice/")]
    public IActionResult UpdateEmployee(EmployeeDTO EmployeeDTO)
    {
        var update = _employeeService.Edit(EmployeeDTO);
        if (!update)
        {
            return BadRequest(new ResponseHandler<EmployeeDTO>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Bad Connections, Data Failed to Update"
            });
        }
        return Ok(new ResponseHandler<EmployeeDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully update the data",
            Data = EmployeeDTO
        });
    }

    [HttpDelete("Delete/{guid}")]
    public IActionResult DeleteEmployee(Guid guid) 
    { 
        var delete = _employeeService.Delete(guid);
        switch(delete)
        {
            case -1:
                return NotFound(new ResponseHandler<EmployeeDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "The data that is to be deleted was not found."
                });
            case 0:
                return BadRequest(new ResponseHandler<EmployeeDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Poor Connections: Data Deletion Failed"
                });
        }
        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "The data was successfully deleted.",
            Data = delete
        });
    }
}
