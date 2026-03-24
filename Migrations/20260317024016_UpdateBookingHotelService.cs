using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingHotelService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingHotelServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "BookingHotelServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookingHotelServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "BookingHotelServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "BookingHotelServices",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookingHotelServices");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BookingHotelServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookingHotelServices");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "BookingHotelServices");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "BookingHotelServices");
        }
    }
}
