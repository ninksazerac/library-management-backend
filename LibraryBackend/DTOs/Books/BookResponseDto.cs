namespace LibraryBackend.DTOs.Books;
public class BookResponseDto
{
    public int BookID { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int PublisherYear { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
    public string AvailabilityStatus { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}