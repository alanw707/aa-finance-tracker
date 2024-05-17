using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAExpenseTracker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColorFromExpenseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColourHex",
                table: "ExpensesCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColourHex",
                table: "ExpensesCategories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
