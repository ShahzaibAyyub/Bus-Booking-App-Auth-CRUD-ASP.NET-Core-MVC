using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace finalDeliverable.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    name = table.Column<string>(nullable: false),
                    passward = table.Column<string>(nullable: false),
                    gender = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    busID = table.Column<string>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    counter = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.busID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    name = table.Column<string>(nullable: false),
                    passward = table.Column<string>(nullable: false),
                    gender = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    bookingID = table.Column<string>(nullable: false),
                    busIdForeignkey = table.Column<string>(nullable: true),
                    userIdForeignkey = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.bookingID);
                    table.ForeignKey(
                        name: "FK_Booking_Bus_busIdForeignkey",
                        column: x => x.busIdForeignkey,
                        principalTable: "Bus",
                        principalColumn: "busID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_User_userIdForeignkey",
                        column: x => x.userIdForeignkey,
                        principalTable: "User",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_busIdForeignkey",
                table: "Booking",
                column: "busIdForeignkey");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_userIdForeignkey",
                table: "Booking",
                column: "userIdForeignkey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
