using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiningRoomDatabaseImplement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storekeepers",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storekeepers", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Cooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorekeeperLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cooks_Storekeepers_StorekeeperLogin",
                        column: x => x.StorekeeperLogin,
                        principalTable: "Storekeepers",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorekeeperLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Storekeepers_StorekeeperLogin",
                        column: x => x.StorekeeperLogin,
                        principalTable: "Storekeepers",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lunches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkerLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lunches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lunches_Workers_WorkerLogin",
                        column: x => x.WorkerLogin,
                        principalTable: "Workers",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calorie = table.Column<int>(type: "int", nullable: false),
                    Wishes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkerLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Workers_WorkerLogin",
                        column: x => x.WorkerLogin,
                        principalTable: "Workers",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CookId = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCooks_Cooks_CookId",
                        column: x => x.CookId,
                        principalTable: "Cooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCooks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LunchProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LunchId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LunchProducts_Lunches_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cutleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    WorkerLogin = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cutleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cutleries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cutleries_Workers_WorkerLogin",
                        column: x => x.WorkerLogin,
                        principalTable: "Workers",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LunchOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LunchId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LunchOrders_Lunches_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Lunches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cooks_StorekeeperLogin",
                table: "Cooks",
                column: "StorekeeperLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Cutleries_OrderId",
                table: "Cutleries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cutleries_WorkerLogin",
                table: "Cutleries",
                column: "WorkerLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Lunches_WorkerLogin",
                table: "Lunches",
                column: "WorkerLogin");

            migrationBuilder.CreateIndex(
                name: "IX_LunchOrders_LunchId",
                table: "LunchOrders",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchOrders_OrderId",
                table: "LunchOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchProducts_LunchId",
                table: "LunchProducts",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchProducts_ProductId",
                table: "LunchProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WorkerLogin",
                table: "Orders",
                column: "WorkerLogin");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCooks_CookId",
                table: "ProductCooks",
                column: "CookId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCooks_ProductId",
                table: "ProductCooks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorekeeperLogin",
                table: "Products",
                column: "StorekeeperLogin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cutleries");

            migrationBuilder.DropTable(
                name: "LunchOrders");

            migrationBuilder.DropTable(
                name: "LunchProducts");

            migrationBuilder.DropTable(
                name: "ProductCooks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Lunches");

            migrationBuilder.DropTable(
                name: "Cooks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Storekeepers");
        }
    }
}
