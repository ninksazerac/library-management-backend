using LibraryBackend.DTOs.Books;
using LibraryBackend.DTOs.Search;

namespace LibraryBackend.Services.Interfaces;

public interface ISearchService
{
    Task<List<BookResponseDto>> SearchBooksAsync(
        SearchBooksRequestDto request
    );
}