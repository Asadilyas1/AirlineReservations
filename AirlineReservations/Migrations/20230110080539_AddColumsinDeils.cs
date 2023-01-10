using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservations.Migrations
{
    public partial class AddColumsinDeils : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "bookingDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "bookingDetails");
        }
    }
}
