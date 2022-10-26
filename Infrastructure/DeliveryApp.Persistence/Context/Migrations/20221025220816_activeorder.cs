using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistence.Context.Migrations
{
    public partial class activeorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveOrderId",
                table: "Couriers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveOrderId",
                table: "Couriers");
        }
    }
}
