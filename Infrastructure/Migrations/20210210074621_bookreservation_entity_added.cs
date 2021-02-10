using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class bookreservation_entity_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "BookReservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLendings_BookId",
                table: "BookLendings",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLendings_Books_BookId",
                table: "BookLendings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLendings_Books_BookId",
                table: "BookLendings");

            migrationBuilder.DropIndex(
                name: "IX_BookLendings_BookId",
                table: "BookLendings");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "BookReservations");
        }
    }
}
