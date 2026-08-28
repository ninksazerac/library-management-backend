using LibraryBackend.DTOs.Books;
using LibraryBackend.DTOs.Search;
using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;
using LibraryBackend.Exceptions;
using Microsoft.AspNetCore.Http.Connections;
public class SearchService : ISearchService
{
    private readonly IBookRepository _bookRepository;

    public SearchService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookResponseDto>> SearchBooksAsync(
        SearchBooksRequestDto request)
    {
        var books = await _bookRepository.SearchAsync(
            request.Title,
            request.Author,
            request.ISBN,
            request.Category,
            request.AvailabilityStatus);

        return books.Select(MapToResponse).ToList();
    }

    private BookResponseDto MapToResponse(Book book)
    {
        return new BookResponseDto
        {
            BookID = book.BookID,
            ISBN = book.ISBN,
            Title = book.Title,
            Author = book.Author,
            Publisher = book.Publisher,
            PublisherYear = book.PublisherYear,
            Category = book.Category,
            Location = book.Location,
            AvailabilityStatus = book.AvailabilityStatus
        };
    }
}