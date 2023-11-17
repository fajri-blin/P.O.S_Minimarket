using API.DTOs.TransactionsDTO;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var transactions = _transactionService.GetAll();
        if (transactions == null)
        {
            return NotFound(new ResponseHandler<TransactionDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<TransactionDTO>>()
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = transactions
        });
    }
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var transaction = _transactionService.GetDetailTransaction(guid);
        if (transaction == null)
        {
            return NotFound(new ResponseHandler<TransactionDTO>()
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseHandler<TransactionDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = transaction
        });
    }

    [HttpPost("AddTransaction/")]
    public IActionResult AddTransaction(NewTransactionDTO transactionDTO)
    {
        var transaction = _transactionService.Create(transactionDTO);
        if (transaction == null)
        {
            return BadRequest(new ResponseHandler<TransactionDTO>()
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed to Create Data"
            });
        }
        return Ok(new ResponseHandler<NewTransactionDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Successfully Created",
            Data = transactionDTO
        });
    }

    [HttpPut("UpdateTransaction/")]
    public IActionResult UpdateTransaction(TransactionDTO transactionDTO)
    {
        var isUpdated = _transactionService.Edit(transactionDTO);
        switch(isUpdated)
        {
            case (int)HttpStatusCode.BadRequest:
                return BadRequest(new ResponseHandler<TransactionDTO>()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Failed to Updated"
                });
            case (int)HttpStatusCode.NotFound:
                return NotFound(new ResponseHandler<TransactionDTO>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
        }
        return Ok(new ResponseHandler<TransactionDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Successfully Edited"
        });
    }

    [HttpDelete("DeleteTransaction")]
    public IActionResult DeleteTransaction(Guid guid) 
    {
        var isDelete = _transactionService.Delete(guid);
        switch (isDelete)
        {
            case (int)HttpStatusCode.BadRequest:
                return BadRequest(new ResponseHandler<TransactionDTO>()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data Failed to Updated"
                });
            case (int)HttpStatusCode.NotFound:
                return NotFound(new ResponseHandler<TransactionDTO>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
        }
        return Ok(new ResponseHandler<TransactionDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Successfully Edited"
        });
    }
}
