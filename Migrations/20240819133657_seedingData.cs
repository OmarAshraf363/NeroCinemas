using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nero.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "FirstName", "LastName", "News", "ProfilePucture" },
                values: new object[,]
                {
                    { 1, "Action star", "John", "Doe", "Starring in Action Movie 1", "john-doe.jpg" },
                    { 2, "Comedy queen", "Jane", "Smith", "Leading role in Comedy Movie 1", "jane-smith.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Address", "CinemaLogo", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "123 City Center Blvd", "cinema-a-logo.png", "Cinema in City Center", "Cinema A" },
                    { 2, "456 Downtown St", "cinema-b-logo.png", "Downtown Cinema", "Cinema B" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CategoryId", "CinemaId", "Description", "EndDate", "ImgUrl", "MovieStatus", "Name", "NumOfVisit", "Price", "StartDate", "TrailerUrl" },
                values: new object[,]
                {
                    { 1, 1, 1, "Exciting action movie", new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie1.png", 1, "Action Movie 1", 100, 10.99, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://trailer-url.com/action-movie-1" },
                    { 2, 2, 2, "Hilarious comedy movie", new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "movie2.png", 0, "Comedy Movie 1", 50, 8.9900000000000002, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://trailer-url.com/comedy-movie-1" }
                });

            migrationBuilder.InsertData(
                table: "ActorsMovie",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActorsMovie",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ActorsMovie",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
