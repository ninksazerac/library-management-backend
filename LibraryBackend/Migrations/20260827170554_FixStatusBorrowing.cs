using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusBorrowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_Status",
                table: "Borrowings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowedAt",
                table: "Borrowings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Borrowings_Status",
                table: "Borrowings",
                sql: "Status IN ('Borrowed', 'Returned', 'Overdue')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Borrowings_Status",
                table: "Borrowings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BorrowedAt",
                table: "Borrowings",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_Status",
                table: "Borrowings",
                sql: "Status IN ('Borrowed', 'Returned', 'Overdue')");
        }
    }
}
