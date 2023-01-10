using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservations.Migrations
{
    public partial class updateBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketPrice",
                table: "bookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "bookingDetails");
        }
    }
}
