using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserID);
            entity.Property(u => u.UserID)
                .ValueGeneratedOnAdd();

            entity.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();
            entity.HasIndex(u => u.Username)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(u => u.Role)
                .HasMaxLength(30)
                .IsRequired();

            entity.ToTable("Users", table =>
            {
                table.HasCheckConstraint(
                    "CK_Users_Role",
                    "Role IN ('Administrator', 'EndUser')"
                );
            });

        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.BookID);
            entity.Property(b => b.BookID)
                .ValueGeneratedOnAdd();

            entity.Property(b => b.ISBN)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(b => b.Author)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(b => b.Publisher)
                .HasMaxLength(100);

            entity.Property(b => b.PublisherYear)
                .IsRequired();

            entity.Property(b => b.Category)
                .HasMaxLength(50);

            entity.Property(b => b.Location)
                .HasMaxLength(100);

            entity.Property(b => b.AvailabilityStatus)
                .HasMaxLength(30)
                .IsRequired();
            entity.ToTable("Books", table =>
            {
                table.HasCheckConstraint(
                    "CK_Books_AvailabilityStatus",
                    "AvailabilityStatus IN ('Available', 'Borrowed', 'Lost', 'Damaged')"
                );
            });

            entity.Property(b => b.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(b => b.UpdatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(b => b.Details)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(b => b.BorrowingID);
            entity.Property(b => b.BorrowingID)
                .ValueGeneratedOnAdd();

            entity.Property(b => b.BorrowedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(b => b.DueAt)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(b => b.ReturnedAt)
                .HasColumnType("datetime2")
                .IsRequired(false);

            entity.Property(b => b.Status)
                .HasMaxLength(30)
                .IsRequired();
            entity.ToTable("Borrowings", table =>
            {
                table.HasCheckConstraint(
                    "CK_Borrowings_Status",
                    "Status IN ('Borrowed', 'Returned', 'Overdue')"
                );
            });

            entity.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserID);

            entity.HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookID);
        });
    }
}