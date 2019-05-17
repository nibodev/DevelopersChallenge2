using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class TransactionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ImportedFiles");

            migrationBuilder.AddColumn<bool>(
                name: "Reconciled",
                table: "Transactions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reconciled",
                table: "Transactions");

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "ImportedFiles",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
