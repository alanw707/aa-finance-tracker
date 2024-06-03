using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAExpenseTracker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCustodianBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustodianBankId",
                table: "Investments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustodianBankName",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustodianBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustodianBanks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_CustodianBankId",
                table: "Investments",
                column: "CustodianBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_CustodianBanks_CustodianBankId",
                table: "Investments",
                column: "CustodianBankId",
                principalTable: "CustodianBanks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_CustodianBanks_CustodianBankId",
                table: "Investments");

            migrationBuilder.DropTable(
                name: "CustodianBanks");

            migrationBuilder.DropIndex(
                name: "IX_Investments_CustodianBankId",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "CustodianBankId",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "CustodianBankName",
                table: "Investments");
        }
    }
}
