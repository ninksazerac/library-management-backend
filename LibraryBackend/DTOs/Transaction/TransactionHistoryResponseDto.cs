namespace LibraryBackend.DTOs.Transaction;

public class TransactionHistoryResponseDto
{
    public int BorrowingID {get; set;}
    public int UserID {get; set;}
    public string Username {get; set;} = string.Empty;
    public int BookID {get; set;}
    public string ISBN { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
    public DateTime DueAt { get; set; }
    public DateTime BorrowedAt {get; set;}
    public DateTime? ReturnedAt {get; set;}
    public string Status {get; set;} = string.Empty;

}