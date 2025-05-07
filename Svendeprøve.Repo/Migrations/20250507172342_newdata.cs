using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Svendeprøve.Repo.Migrations
{
    /// <inheritdoc />
    public partial class newdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Hall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hall", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    HallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Hall_HallId",
                        column: x => x.HallId,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 12, "Adventure" },
                    { 14, "Fantasy" },
                    { 16, "Animation" },
                    { 18, "Drama" },
                    { 27, "Horror" },
                    { 28, "Action" },
                    { 35, "Comedy" },
                    { 36, "History" },
                    { 37, "Western" },
                    { 53, "Thriller" },
                    { 80, "Crime" },
                    { 99, "Documentary" },
                    { 878, "Science Fiction" },
                    { 9648, "Mystery" },
                    { 10402, "Music" },
                    { 10749, "Romance" },
                    { 10751, "Family" },
                    { 10752, "War" },
                    { 10770, "TV Movie" }
                });

            migrationBuilder.InsertData(
                table: "Hall",
                columns: new[] { "Id", "Name", "SeatCount" },
                values: new object[,]
                {
                    { 1, "Hall 1", 9 },
                    { 2, "Hall 2", 4 },
                    { 3, "Hall 3", 4 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "kasper@mail.com", "Kasper", "placeholder1", "123456789" },
                    { 2, "sofia@mail.com", "Sofia", "placeholder2", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "HallId", "IsReserved", "Row", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 1 },
                    { 2, 1, false, 1, 2 },
                    { 3, 1, false, 1, 3 },
                    { 4, 1, false, 2, 4 },
                    { 5, 1, false, 2, 5 },
                    { 6, 1, false, 2, 6 },
                    { 7, 1, true, 3, 7 },
                    { 8, 1, true, 3, 8 },
                    { 9, 1, false, 3, 9 },
                    { 10, 2, false, 1, 1 },
                    { 11, 2, true, 1, 2 },
                    { 12, 2, false, 2, 3 },
                    { 13, 2, false, 2, 4 },
                    { 14, 3, true, 1, 1 },
                    { 15, 3, false, 1, 2 },
                    { 16, 3, false, 2, 3 },
                    { 17, 3, false, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "Id", "IsCanceled", "Price", "SeatId", "UserId" },
                values: new object[,]
                {
                    { 1, false, 75, 1, 1 },
                    { 2, false, 75, 2, 1 },
                    { 3, false, 95, 4, 2 },
                    { 4, true, 75, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_HallId",
                table: "Seat",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Hall");
        }
    }
}
