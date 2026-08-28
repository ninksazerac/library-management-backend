using LibraryBackend.DTOs.Transaction;
using LibraryBackend.Repositories.Interfaces;
using LibraryBackend.Services.Interfaces;

namespace LibraryBackend.Services;

public class TransactionService : ITransactionService
{
    private readonly IBorrowingRepository _borrowingRepository;

    public TransactionService(
        IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<List<TransactionHistoryResponseDto>>
        GetTransactionHistoryAsync()
    {
        var borrowings =
            await _borrowingRepository.GetHistoryAsync();

        return borrowings.Select(b =>
            new TransactionHistoryResponseDto
            {
                BorrowingID = b.BorrowingID,
                UserID = b.UserID,
                Username = b.User.Username,
                BookID = b.BookID,
                ISBN = b.Book.ISBN,
                BookTitle = b.Book.Title,
                BorrowedAt = b.BorrowedAt,
                DueAt = b.DueAt,
                ReturnedAt = b.ReturnedAt,
                Status = b.Status
            }).ToList();
    }

    public async Task<List<TransactionHistoryResponseDto>>
    GetMyTransactionHistoryAsync(int userId)
    {
        var borrowings =
            await _borrowingRepository
                .GetByUserIdAsync(userId);

        return borrowings.Select(b =>
            new TransactionHistoryResponseDto
            {
                BorrowingID = b.BorrowingID,
                UserID = b.UserID,
                Username = b.User.Username,
                BookID = b.BookID,
                ISBN = b.Book.ISBN,
                BookTitle = b.Book.Title,
                BorrowedAt = b.BorrowedAt,
                DueAt = b.DueAt,
                ReturnedAt = b.ReturnedAt,
                Status = b.Status
            }).ToList();
    }
}