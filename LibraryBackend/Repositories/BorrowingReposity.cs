using LibraryBackend.Data;
using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Repositories;

public class BorrowingRepository : IBorrowingRepository
{
    private readonly AppDbContext _context;
    public BorrowingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Borrowing> AddAsync(Borrowing borrowing)
    {
        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();
        return borrowing;
    }

    public async Task<Borrowing?> GetByIdAsync(int borrowingId)
    {
        return await _context.Borrowings.FirstOrDefaultAsync(b => b.BorrowingID == borrowingId);
    }

    public async Task<List<Borrowing>> GetByUserIdAsync(int userId)
    {
        return await _context.Borrowings
            .Include(b => b.User)
            .Include(b => b.Book)
            .Where(b => b.UserID == userId)
            .OrderByDescending(b => b.BorrowedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(Borrowing borrowing)
    {
        _context.Borrowings.Update(borrowing);

        await _context.SaveChangesAsync();
    }
    public async Task<List<Borrowing>> GetHistoryAsync()
    {
        return await _context.Borrowings
            .Include(b => b.User)
            .Include(b => b.Book)
            .OrderByDescending(b => b.BorrowedAt)
            .ToListAsync();
    }
}