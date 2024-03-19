using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aa_finance_tracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseTypePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseTypes",
                table: "ExpenseTypes",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseTypes",
                table: "ExpenseTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpenseTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
