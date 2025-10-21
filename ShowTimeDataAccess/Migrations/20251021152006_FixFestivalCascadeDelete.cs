using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixFestivalCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Festivals_FestivalId",
                table: "Lineups");

            // Recreate with CASCADE delete
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Festivals_FestivalId",
                table: "Lineups",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
