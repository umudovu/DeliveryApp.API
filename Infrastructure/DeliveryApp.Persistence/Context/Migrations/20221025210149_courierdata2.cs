using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistence.Context.Migrations
{
    public partial class courierdata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courier_AspNetUsers_AppUserId",
                table: "Courier");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Courier_CourierId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courier",
                table: "Courier");

            migrationBuilder.RenameTable(
                name: "Courier",
                newName: "Couriers");

            migrationBuilder.RenameIndex(
                name: "IX_Courier_AppUserId",
                table: "Couriers",
                newName: "IX_Couriers_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_AspNetUsers_AppUserId",
                table: "Couriers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couriers_AspNetUsers_AppUserId",
                table: "Couriers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Couriers",
                table: "Couriers");

            migrationBuilder.RenameTable(
                name: "Couriers",
                newName: "Courier");

            migrationBuilder.RenameIndex(
                name: "IX_Couriers_AppUserId",
                table: "Courier",
                newName: "IX_Courier_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courier",
                table: "Courier",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courier_AspNetUsers_AppUserId",
                table: "Courier",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Courier_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "Courier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
