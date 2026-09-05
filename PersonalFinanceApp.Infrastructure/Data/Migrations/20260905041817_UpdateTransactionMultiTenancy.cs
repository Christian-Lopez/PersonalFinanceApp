using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionMultiTenancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDateUtc",
                table: "transactions",
                newName: "TransactionDate");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "transactions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "transactions",
                newName: "TransactionDateUtc");
        }
    }
}
