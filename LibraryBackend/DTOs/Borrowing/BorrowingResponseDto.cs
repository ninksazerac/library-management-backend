namespace LibraryBackend.DTOs.Borrowing;

public class BorrowingResponseDto
{
    public int BorrowingID { get; set; }

    public int UserID { get; set; }

    public int BookID { get; set; }

    public DateTime BorrowedAt { get; set; }

    public DateTime DueAt { get; set; }

    public DateTime? ReturnedAt { get; set; }

    public string Status { get; set; } = string.Empty;
}