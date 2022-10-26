using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistence.Context.Migrations
{
    public partial class shippedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippedStatus",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippedStatus",
                table: "Orders");
        }
    }
}
