using System.ComponentModel.DataAnnotations;
namespace LibraryBackend.DTOs.Books;
public class CreateBookRequestDto
{
    [Required]
    [MaxLength(20)]
    public string ISBN { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Publisher { get; set; } = string.Empty;

    [Range(1000, 2100)]
    public int PublisherYear { get; set; }

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;
}