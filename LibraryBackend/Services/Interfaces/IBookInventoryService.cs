using LibraryBackend.DTOs.Books;

namespace LibraryBackend.Services.Interfaces;

public interface IBookInventoryService
{
    Task<BookResponseDto> CreateBookAsync(
        CreateBookRequestDto request);

    Task<BookResponseDto?> GetBookAsync(
        int bookId);

    Task<List<BookResponseDto>> GetBooksAsync();

    Task<BookResponseDto?> UpdateBookAsync(int bookId, UpdateBookRequestDto request);

    Task<bool> DeleteBookAsync(int bookId);
    
}