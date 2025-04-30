using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntPaymentAPI.Migrations
{
    public partial class CreateTransactionDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payments",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "PaymentDate");
        }
    }
}
