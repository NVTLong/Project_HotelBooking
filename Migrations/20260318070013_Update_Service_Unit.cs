using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class Update_Service_Unit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOneTime",
                table: "HotelServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "HotelServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOneTime",
                table: "HotelServices");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "HotelServices");
        }
    }
}
