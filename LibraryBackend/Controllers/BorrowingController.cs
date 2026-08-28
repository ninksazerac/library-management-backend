using LibraryBackend.DTOs.Borrowing;
using LibraryBackend.Services.Interfaces;
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
        var result =
            await _borrowingService.BorrowBookAsync(request);

        return Ok(result);
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var result =
            await _borrowingService.ReturnBookAsync(id);

        return Ok(result);
    }
}