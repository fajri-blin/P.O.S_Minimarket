﻿using API.DTOs.ProductDTO;
using API.DTOs.RolesDTO;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

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
            return NotFound(new ResponseHandler<RoleDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<RoleDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listRole
        });
    }

    [HttpPost("Create")]
    public IActionResult Create(NewRoleDTO newRoleDTO)
    {
        int status = _roleService.Create(newRoleDTO);
        switch(status)
        {
            case 0 :
                return BadRequest(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Create"
                });
            case 1 :
                return Ok(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Successfully create the data",
                });
            default :
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Unexpected status value"
                });
        }
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Guid guid)
    {
        var delete = _roleService.Delete(guid);
        switch(delete)
        {
            case -1:
                return NotFound(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data that want to delete is not found"
                });
            case 0:
                return BadRequest(new ResponseHandler<RoleDTO>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Bad Connections, Data Failed to Delete"
                });
            default: break;
        }
        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
            Data = delete
        });
    }
}
