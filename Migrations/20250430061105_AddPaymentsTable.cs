using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntPaymentAPI.Migrations
{
    public partial class AddPaymentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "PaymentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
