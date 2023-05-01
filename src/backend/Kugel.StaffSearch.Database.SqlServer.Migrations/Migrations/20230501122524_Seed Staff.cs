using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kugel.StaffSearch.Database.SqlServer.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StaffMember",
                columns: new[] { "Id", "FirstName", "LastName", "PersonalId" },
                values: new object[,]
                {
                    { new Guid("56df7680-047e-4796-9f2e-00e3094b212a"), "Janek", "Fellien", "2023-01-01-03" },
                    { new Guid("c6fea08e-620d-4a64-915a-77cad7d4b140"), "Philipp", "Bauknecht", "2023-01-01-02" },
                    { new Guid("e9774bcb-191a-48cc-8295-23dfa452bfad"), "Petra", "Bauknecht", "2023-01-01-01" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StaffMember",
                keyColumn: "Id",
                keyValue: new Guid("56df7680-047e-4796-9f2e-00e3094b212a"));

            migrationBuilder.DeleteData(
                table: "StaffMember",
                keyColumn: "Id",
                keyValue: new Guid("c6fea08e-620d-4a64-915a-77cad7d4b140"));

            migrationBuilder.DeleteData(
                table: "StaffMember",
                keyColumn: "Id",
                keyValue: new Guid("e9774bcb-191a-48cc-8295-23dfa452bfad"));
        }
    }
}
