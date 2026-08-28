using LibraryBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibraryBackend.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(
        ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("history")]
    public async Task<IActionResult> GetTransactionHistory()
    {
        var result =
            await _transactionService
                .GetTransactionHistoryAsync();

        return Ok(result);
    }

    [Authorize(Roles = "EndUser")]
    [HttpGet("my-history")]
    public async Task<IActionResult> GetMyHistory()
    {
        var userIdClaim = User.FindFirst(
    ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = int.Parse(userIdClaim.Value);

        var result =
            await _transactionService
                .GetMyTransactionHistoryAsync(userId);

        return Ok(result);
    }
}