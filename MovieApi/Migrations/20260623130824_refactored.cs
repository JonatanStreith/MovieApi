using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    MovieDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Synopsis = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Budget = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    MovieTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.MovieDetailsId);
                    table.ForeignKey(
                        name: "FK_Details_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "MovieActors",
                columns: table => new
                {
                    MovieActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActors", x => x.MovieActorId);
                    table.ForeignKey(
                        name: "FK_MovieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "ActorId", "BirthYear", "Name" },
                values: new object[,]
                {
                    { 1, 1965, "Robert Downey Jr." },
                    { 2, 1972, "Gwyneth Paltrow" },
                    { 3, 1969, "Terrence Howard" },
                    { 4, 1952, "Mickey Rourke" },
                    { 5, 1967, "Guy Pearce" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Duration", "Genre", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 126, "Action", "Iron Man 1", 2008 },
                    { 2, 124, "Action", "Iron Man 2", 2010 },
                    { 3, 130, "Action", "Iron Man 3", 2013 }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "MovieDetailsId", "Budget", "Language", "MovieId", "MovieTitle", "Synopsis" },
                values: new object[,]
                {
                    { 1, 150, "english", 1, "Iron Man 1", "After being captured by terrorists following a missile demonstration in Afghanistan, multi-billionaire Tony Stark uses his brilliant intellect to devise a powered armor to escape. Being an irresponsible, wealthy playboy before, he (literally) has a change of heart regarding his company policies and dedicates himself to cleaning up Stark Industries' patented weapons and taking care of the terrorist group that got their hands on them. To do so, he builds an even better suit of armor. However, not everyone in his company likes the new direction he's chosen." },
                    { 2, 170, "english", 2, "Iron Man 2", "Several months after the events of Iron Man, the film deals with the consequences of Tony Stark outing himself as Iron Man and becoming the world's newest defender. His first major issue is Congressional hearings about sharing his tech, with rival (and perpetually second-place to Tony) industrialist Justin Hammer (Sam Rockwell) standing the most to gain. Despite their best efforts, Tony is untouchable: unbeatable in conferences and unstoppable as Iron Man. But his invincibility is tested by Ivan Vanko/Whiplash (Mickey Rourke), a man with a grudge against the Stark empire who is more than capable of challenging Tony's genius, as Tony is also dealing with a slowly fatal medical condition resulting from his arc reactor implant." },
                    { 3, 200, "english", 3, "Iron Man 3", "When an enemy from the past targets that which industrialist Tony Stark holds most dear, he must rely on his ingenuity to protect those closest to him. Still haunted by the events from the Battle of New York, he must confront challenges from not only this old adversary but from himself as well, and finally answer a question which has plagued him from the beginning: Is he the one who defines the Iron Man suit? Or is it the suit that defines him?" }
                });

            migrationBuilder.InsertData(
                table: "MovieActors",
                columns: new[] { "MovieActorId", "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 1, 2 },
                    { 5, 2, 2 },
                    { 6, 4, 2 },
                    { 7, 1, 3 },
                    { 8, 2, 3 },
                    { 9, 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "MovieId", "MovieTitle", "Rating", "ReviewerName" },
                values: new object[,]
                {
                    { 1, "OMIGOD THIS IS THE BEST MOVIE EVER!", 1, "Aron Man 1", 5, "Chester A. Bum" },
                    { 2, "A distasteful look into the horrifying military complex. Also, Marvel just keeps pumping these out. This is, what, the fifth one in the series?", 1, "Bron Man 1", 1, "Snooty McNitpick" },
                    { 3, "Tony looks so hot in this one. Shame about the ending. I wrote a fanfic that fixed everything, read it at http://wwwfanfic.con/340694hgio/a/", 3, "Cron Man 3", 5, "StarkLovr4908" },
                    { 4, "A fascinating adaptation of long-running comics continuity adapted into film. A shame TONY DIES IN ENDGAME!", 1, "Dron Man 1", 5, "Doug Spoilerton" },
                    { 5, "Are all these reviews made up? Whatever. I liked the movie.", 2, "Eron Man 2", 4, "Richard Normalman" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_MovieId",
                table: "Details",
                column: "MovieId",
                unique: true,
                filter: "[MovieId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_ActorId",
                table: "MovieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_MovieId",
                table: "MovieActors",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "MovieActors");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
