using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class BookReservationEntity_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_BookId",
                table: "BookReservations",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_Books_BookId",
                table: "BookReservations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_Books_BookId",
                table: "BookReservations");

            migrationBuilder.DropIndex(
                name: "IX_BookReservations_BookId",
                table: "BookReservations");
        }
    }
}
