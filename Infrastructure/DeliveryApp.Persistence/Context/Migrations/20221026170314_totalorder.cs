using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistence.Context.Migrations
{
    public partial class totalorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalOrder",
                table: "Couriers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrder",
                table: "Couriers");
        }
    }
}
