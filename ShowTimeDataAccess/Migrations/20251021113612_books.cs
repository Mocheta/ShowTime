using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings",
                column: "TicketId",
                unique: true);
        }
    }
}
