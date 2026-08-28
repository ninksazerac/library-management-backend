using LibraryBackend.DTOs.Borrowing;
using LibraryBackend.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryBackend.Controllers;

[ApiController]
[Route("api/borrowings")]
public class BorrowingsController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;

    public BorrowingsController(
        IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }

    [Authorize(Roles = "EndUser")]
    [HttpPost]
    public async Task<IActionResult> BorrowBook(
        BorrowBookRequestDto request)
    {
        var userIdClaim = User.FindFirst(
        ClaimTypes.NameIdentifier
    );

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = int.Parse(userIdClaim.Value);

        var result = await _borrowingService
            .BorrowBookAsync(userId, request);

        return Ok(result);
    }
    [Authorize(Roles = "EndUser")]
    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var result =
            await _borrowingService.ReturnBookAsync(id);

        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("assign")]
    public async Task<IActionResult> AssignBook(
    AssignBookRequestDto request)
    {
        var result =
            await _borrowingService
                .AssignBookAsync(request);

        return Ok(result);
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPost("{id}/process-return")]
    public async Task<IActionResult> ProcessReturn(int id)
    {
        var result =
            await _borrowingService
                .ProcessReturnAsync(id);

        return Ok(result);
    }
}