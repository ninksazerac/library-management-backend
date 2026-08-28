using LibraryBackend.Data;
using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> GetByIdAsync(int bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.BookID == bookId);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

    }

    public async Task<Book?> GetByISBNAsync(string isbn)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    public async Task<List<Book>> SearchAsync(
        string? title,
        string? author,
        string? isbn,
        string? category,
        string? availabilityStatus)
    {
        var query = _context.Books.AsQueryable();
        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }
        if (!string.IsNullOrWhiteSpace(isbn))
        {
            query = query.Where(b => b.ISBN.Contains(isbn));
        }
        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(b => b.Category.Contains(category));
        }
        if (!string.IsNullOrWhiteSpace(availabilityStatus))
        {
            query = query.Where(b => b.AvailabilityStatus.Contains(availabilityStatus));
        }

        return await query.ToListAsync();
    }
}