using Microsoft.EntityFrameworkCore.Migrations;

namespace DiningRoomDatabaseImplement.Migrations
{
    public partial class RenameStorekeeperToManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_Storekeepers_StorekeeperLogin",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storekeepers_StorekeeperLogin",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StorekeeperLogin",
                table: "Products",
                newName: "ManagerLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Products_StorekeeperLogin",
                table: "Products",
                newName: "IX_Products_ManagerLogin");

            migrationBuilder.RenameColumn(
                name: "StorekeeperLogin",
                table: "Cooks",
                newName: "ManagerLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Cooks_StorekeeperLogin",
                table: "Cooks",
                newName: "IX_Cooks_ManagerLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_Storekeepers_ManagerLogin",
                table: "Cooks",
                column: "ManagerLogin",
                principalTable: "Storekeepers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storekeepers_ManagerLogin",
                table: "Products",
                column: "ManagerLogin",
                principalTable: "Storekeepers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_Storekeepers_ManagerLogin",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storekeepers_ManagerLogin",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ManagerLogin",
                table: "Products",
                newName: "StorekeeperLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ManagerLogin",
                table: "Products",
                newName: "IX_Products_StorekeeperLogin");

            migrationBuilder.RenameColumn(
                name: "ManagerLogin",
                table: "Cooks",
                newName: "StorekeeperLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Cooks_ManagerLogin",
                table: "Cooks",
                newName: "IX_Cooks_StorekeeperLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_Storekeepers_StorekeeperLogin",
                table: "Cooks",
                column: "StorekeeperLogin",
                principalTable: "Storekeepers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storekeepers_StorekeeperLogin",
                table: "Products",
                column: "StorekeeperLogin",
                principalTable: "Storekeepers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
