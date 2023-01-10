using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservations.Migrations
{
    public partial class UpdateTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "SinglePrice",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TwoWaysPrice",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinglePrice",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TwoWaysPrice",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "TicketPrice",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
