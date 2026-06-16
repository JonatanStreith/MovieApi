using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class MovieActorRelationWithAllFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieActor_MovieId",
                table: "MovieActor",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Movie_MovieId",
                table: "MovieActor",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Movie_MovieId",
                table: "MovieActor");

            migrationBuilder.DropIndex(
                name: "IX_MovieActor_MovieId",
                table: "MovieActor");
        }
    }
}
