using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAExpenseTracker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvestmentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_InvestmentsTypes_TypeName",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentsTypes",
                table: "InvestmentsTypes");

            migrationBuilder.RenameTable(
                name: "InvestmentsTypes",
                newName: "InvestmentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentTypes",
                table: "InvestmentTypes",
                column: "TypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_InvestmentTypes_TypeName",
                table: "Investments",
                column: "TypeName",
                principalTable: "InvestmentTypes",
                principalColumn: "TypeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_InvestmentTypes_TypeName",
                table: "Investments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvestmentTypes",
                table: "InvestmentTypes");

            migrationBuilder.RenameTable(
                name: "InvestmentTypes",
                newName: "InvestmentsTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvestmentsTypes",
                table: "InvestmentsTypes",
                column: "TypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_InvestmentsTypes_TypeName",
                table: "Investments",
                column: "TypeName",
                principalTable: "InvestmentsTypes",
                principalColumn: "TypeName");
        }
    }
}
