using LibraryBackend.DTOs.Transaction;

namespace LibraryBackend.Services.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionHistoryResponseDto>>
        GetTransactionHistoryAsync();

    // * EndUser
    Task<List<TransactionHistoryResponseDto>>
    GetMyTransactionHistoryAsync(int userId);
}