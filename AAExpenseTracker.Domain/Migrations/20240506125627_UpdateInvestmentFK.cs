using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAExpenseTracker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvestmentFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvestmentTypeName",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentTypeName",
                table: "Investments");
        }
    }
}
