using LibraryBackend.DTOs.Books;
using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;
using LibraryBackend.Exceptions;

namespace LibraryBackend.Services;

public class BookInventoryService : IBookInventoryService
{
    private readonly IBookRepository _bookRepository;

    public BookInventoryService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookResponseDto> CreateBookAsync(
        CreateBookRequestDto request)
    {
        var existingBook = await _bookRepository
        .GetByISBNAsync(request.ISBN);

        if (existingBook != null)
        {
            throw new ConflictException(
                "A book with this ISBN already exists.");
        }

        var book = new Book
        {
            ISBN = request.ISBN,
            Title = request.Title,
            Author = request.Author,
            Publisher = request.Publisher,
            PublisherYear = request.PublisherYear,
            Category = request.Category,
            Location = request.Location,

            AvailabilityStatus = "Avaliable",
        };

        var createdBook = await _bookRepository.AddAsync(book);

        return MapToResponse(createdBook);
    }

    public async Task<BookResponseDto?> GetBookAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException(
            $"Book with ID {bookId} was not found.");
        }

        return MapToResponse(book);
    }

    public async Task<List<BookResponseDto>> GetBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books.Select(book => new BookResponseDto
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
        }).ToList();
    }

    public async Task<BookResponseDto?> UpdateBookAsync(int bookId, UpdateBookRequestDto request)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException(
            $"Book with ID {bookId} was not found.");
        }

        var existingBook = await _bookRepository
        .GetByISBNAsync(request.ISBN);

        if (existingBook != null &&
        existingBook.BookID != bookId)
        {
            throw new ConflictException(
                "A book with this ISBN already exists.");
        }


        book.ISBN = request.ISBN;
        book.Title = request.Title;
        book.Author = request.Author;
        book.Publisher = request.Publisher;
        book.PublisherYear = request.PublisherYear;
        book.Category = request.Category;
        book.Location = request.Location;

        book.UpdatedAt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            DateTime.UtcNow,
            "SE Asia Standard Time"
        );

        await _bookRepository.UpdateAsync(book);

        return MapToResponse(book);
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book == null)
        {
            throw new NotFoundException(
            $"Book with ID {bookId} was not found.");
        }

        if (book.AvailabilityStatus == "Borrowed")
        {
            throw new ConflictException(
                "Cannot delete a borrowed book.");
        }

        await _bookRepository.DeleteAsync(book);

        return true;
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