using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id");
        }
    }
}
