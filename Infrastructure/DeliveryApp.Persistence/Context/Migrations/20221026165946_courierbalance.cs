using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistence.Context.Migrations
{
    public partial class courierbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalProfit",
                table: "Couriers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalProfit",
                table: "Couriers");
        }
    }
}
