using LibraryBackend.DTOs.Books;
using LibraryBackend.DTOs.Search;
using LibraryBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryBackend.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookInventoryService _bookService;
    private readonly ISearchService _searchService;

    public BooksController(
        IBookInventoryService bookService,
        ISearchService searchService)
    {
        _bookService = bookService;
        _searchService = searchService;
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequestDto request)
    {
        var result = await _bookService.CreateBookAsync(request);

        return CreatedAtAction(nameof(CreateBook), new { id = result.BookID }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var result = await _bookService.GetBookAsync(id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var result = await _bookService.GetBooksAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(
    int id,
    UpdateBookRequestDto request)
    {
        try
        {
            var result = await _bookService.UpdateBookAsync(
                id,
                request
            );

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Book not found."
            });
        }

        return NoContent();
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks(
    [FromQuery] SearchBooksRequestDto request)
    {
        var result = await _searchService.SearchBooksAsync(request);

        return Ok(result);
    }

}

