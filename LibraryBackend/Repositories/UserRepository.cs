using LibraryBackend.Data;
using LibraryBackend.Models;
using LibraryBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == userId);
    }
}