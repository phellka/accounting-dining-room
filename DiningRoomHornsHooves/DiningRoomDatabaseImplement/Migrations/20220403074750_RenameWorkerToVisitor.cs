using Microsoft.EntityFrameworkCore.Migrations;

namespace DiningRoomDatabaseImplement.Migrations
{
    public partial class RenameWorkerToVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_Storekeepers_ManagerLogin",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Cutleries_Workers_WorkerLogin",
                table: "Cutleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Lunches_Workers_WorkerLogin",
                table: "Lunches");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workers_WorkerLogin",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storekeepers_ManagerLogin",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Visitors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storekeepers",
                table: "Storekeepers");

            migrationBuilder.RenameTable(
                name: "Storekeepers",
                newName: "Managers");

            migrationBuilder.RenameColumn(
                name: "WorkerLogin",
                table: "Orders",
                newName: "VisitorLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_WorkerLogin",
                table: "Orders",
                newName: "IX_Orders_VisitorLogin");

            migrationBuilder.RenameColumn(
                name: "WorkerLogin",
                table: "Lunches",
                newName: "VisitorLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Lunches_WorkerLogin",
                table: "Lunches",
                newName: "IX_Lunches_VisitorLogin");

            migrationBuilder.RenameColumn(
                name: "WorkerLogin",
                table: "Cutleries",
                newName: "VisitorLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Cutleries_WorkerLogin",
                table: "Cutleries",
                newName: "IX_Cutleries_VisitorLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Login");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_Managers_ManagerLogin",
                table: "Cooks",
                column: "ManagerLogin",
                principalTable: "Managers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cutleries_Visitors_VisitorLogin",
                table: "Cutleries",
                column: "VisitorLogin",
                principalTable: "Visitors",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lunches_Visitors_VisitorLogin",
                table: "Lunches",
                column: "VisitorLogin",
                principalTable: "Visitors",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Visitors_VisitorLogin",
                table: "Orders",
                column: "VisitorLogin",
                principalTable: "Visitors",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Managers_ManagerLogin",
                table: "Products",
                column: "ManagerLogin",
                principalTable: "Managers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooks_Managers_ManagerLogin",
                table: "Cooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Cutleries_Visitors_VisitorLogin",
                table: "Cutleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Lunches_Visitors_VisitorLogin",
                table: "Lunches");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Visitors_VisitorLogin",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Managers_ManagerLogin",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Storekeepers");

            migrationBuilder.RenameColumn(
                name: "VisitorLogin",
                table: "Orders",
                newName: "WorkerLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_VisitorLogin",
                table: "Orders",
                newName: "IX_Orders_WorkerLogin");

            migrationBuilder.RenameColumn(
                name: "VisitorLogin",
                table: "Lunches",
                newName: "WorkerLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Lunches_VisitorLogin",
                table: "Lunches",
                newName: "IX_Lunches_WorkerLogin");

            migrationBuilder.RenameColumn(
                name: "VisitorLogin",
                table: "Cutleries",
                newName: "WorkerLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Cutleries_VisitorLogin",
                table: "Cutleries",
                newName: "IX_Cutleries_WorkerLogin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storekeepers",
                table: "Storekeepers",
                column: "Login");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Login);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cooks_Storekeepers_ManagerLogin",
                table: "Cooks",
                column: "ManagerLogin",
                principalTable: "Storekeepers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cutleries_Workers_WorkerLogin",
                table: "Cutleries",
                column: "WorkerLogin",
                principalTable: "Workers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lunches_Workers_WorkerLogin",
                table: "Lunches",
                column: "WorkerLogin",
                principalTable: "Workers",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workers_WorkerLogin",
                table: "Orders",
                column: "WorkerLogin",
                principalTable: "Workers",
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
    }
}
