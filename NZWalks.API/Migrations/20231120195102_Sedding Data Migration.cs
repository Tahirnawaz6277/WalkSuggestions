 using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeddingDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ff597e9-b117-4168-8d0f-f26658ef1b59"), "Meduim" },
                    { new Guid("4f364ded-7401-48f3-9bc5-36c33a111286"), "Hard" },
                    { new Guid("e2f5c606-070e-45fb-b3ed-1fe0e6b7827f"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("29f23766-a9c7-4c52-a62b-8ee629731b15"), "KPK", "Kherber Pakhton khwa", "Kpk.JPG" },
                    { new Guid("417da255-4f11-4bdc-9e17-31a051a9ff91"), "NZ", "Newzeland", "Nweland.JPG" },
                    { new Guid("8fc7f98e-df80-4941-a808-6f999b126bfb"), "JP", "Japan", "Japan.JPG" },
                    { new Guid("da9b0b1f-1cf0-410c-a932-4ce704b200e9"), "NL", "NatherLand", "NatherLand.JPG" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0ff597e9-b117-4168-8d0f-f26658ef1b59"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4f364ded-7401-48f3-9bc5-36c33a111286"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e2f5c606-070e-45fb-b3ed-1fe0e6b7827f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("29f23766-a9c7-4c52-a62b-8ee629731b15"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("417da255-4f11-4bdc-9e17-31a051a9ff91"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8fc7f98e-df80-4941-a808-6f999b126bfb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("da9b0b1f-1cf0-410c-a932-4ce704b200e9"));
        }
    }
}
