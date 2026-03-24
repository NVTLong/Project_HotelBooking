using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class EditBookingHotelService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHotelServices_Bookings_BookingId",
                table: "BookingHotelServices");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "BookingHotelServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookingDetailId",
                table: "BookingHotelServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookingHotelServices_BookingDetailId",
                table: "BookingHotelServices",
                column: "BookingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHotelServices_BookingDetails_BookingDetailId",
                table: "BookingHotelServices",
                column: "BookingDetailId",
                principalTable: "BookingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHotelServices_Bookings_BookingId",
                table: "BookingHotelServices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHotelServices_BookingDetails_BookingDetailId",
                table: "BookingHotelServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingHotelServices_Bookings_BookingId",
                table: "BookingHotelServices");

            migrationBuilder.DropIndex(
                name: "IX_BookingHotelServices_BookingDetailId",
                table: "BookingHotelServices");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "BookingHotelServices");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "BookingHotelServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHotelServices_Bookings_BookingId",
                table: "BookingHotelServices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
