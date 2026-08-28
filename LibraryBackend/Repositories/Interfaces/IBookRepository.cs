using LibraryBackend.DTOs.Search;
using LibraryBackend.Models;

namespace LibraryBackend.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> AddAsync(Book book);
    Task<Book?> GetByIdAsync(int book);
    Task<List<Book>> GetAllAsync();
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<Book?> GetByISBNAsync(string isbn);
    Task<List<Book>> SearchAsync(
        string? title,
        string? author,
        string? isbn,
        string? category,
        string? availabilityStatus);
}