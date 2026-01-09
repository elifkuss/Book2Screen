using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Screen.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    works = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    country = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("author_pkey", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "directors",
                columns: table => new
                {
                    director_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    works = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("directors_pkey", x => x.director_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    mail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    genre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    summary = table.Column<string>(type: "text", nullable: true),
                    orjlanguage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    country = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("books_pkey", x => x.book_id);
                    table.ForeignKey(
                        name: "books_author_id_fkey",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "author_id");
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    score = table.Column<double>(type: "double precision", nullable: true),
                    movietype = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: true),
                    movieduration = table.Column<int>(type: "integer", nullable: true),
                    budget = table.Column<double>(type: "double precision", nullable: true),
                    boxofficerevenue = table.Column<double>(type: "double precision", nullable: true),
                    book_id = table.Column<int>(type: "integer", nullable: true),
                    director_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("movies_pkey", x => x.movie_id);
                    table.ForeignKey(
                        name: "movies_book_id_fkey",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "movies_director_id_fkey",
                        column: x => x.director_id,
                        principalTable: "directors",
                        principalColumn: "director_id");
                });

            migrationBuilder.CreateTable(
                name: "actors",
                columns: table => new
                {
                    actors_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    movie_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("actors_pkey", x => x.actors_id);
                    table.ForeignKey(
                        name: "actors_movie_id_fkey",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "movie_id");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "integer", nullable: false),
                    comment_text = table.Column<string>(type: "text", nullable: true),
                    movie_id = table.Column<int>(type: "integer", nullable: true),
                    book_id = table.Column<int>(type: "integer", nullable: true),
                    id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comments_pkey", x => x.comment_id);
                    table.ForeignKey(
                        name: "comments_book_id_fkey",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "comments_movie_id_fkey",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "movie_id");
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_actors_movie_id",
                table: "actors",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_author_id",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_book_id",
                table: "comments",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_id",
                table: "comments",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_movie_id",
                table: "comments",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_book_id",
                table: "movies",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_director_id",
                table: "movies",
                column: "director_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actors");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "directors");

            migrationBuilder.DropTable(
                name: "author");
        }
    }
}
