using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedWithDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    GuestID = table.Column<int>(type: "int", nullable: true),
                    RoomNumber = table.Column<int>(type: "int", nullable: true),
                    CheckinDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CheckoutDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Booking__73951ACD867D72D2", x => x.BookingID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EventDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventDateTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__7944C870B86AC4A2", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Guest__0C423C32C3A031EA", x => x.GuestID);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: true),
                    CheckinTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    CheckoutTime = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Hotel__46023BBF5CAD8735", x => x.HotelID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A58F2E06A4A", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: true),
                    TypeID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Room__AE10E07B8CAA0ED0", x => x.RoomNumber);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PricePerNight = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoomType__516F0395E615E4F8", x => x.TypeID);
                });


            Seed(migrationBuilder);
        }
        private void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Name", "HotelID" },
                values: new object[,]
                {
                    {"Hotel 1 Manual Seeding" ,1},
                    {"Hotel 2 Manual Seeding" ,2}
                }
             );
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}
