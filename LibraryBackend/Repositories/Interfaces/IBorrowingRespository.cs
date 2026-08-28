using LibraryBackend.Models;

namespace LibraryBackend.Repositories.Interfaces;

public interface IBorrowingRepository
{
    Task<Borrowing> AddAsync(Borrowing borrowing);
    Task<Borrowing?> GetByIdAsync(int borrowingId);
    Task<List<Borrowing>> GetByUserIdAsync(int userId);
    Task UpdateAsync(Borrowing borrowing);
    Task<List<Borrowing>> GetHistoryAsync();
}