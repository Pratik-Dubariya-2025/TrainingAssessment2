using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAssessment.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddingDescriptionToConcert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Concert",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 9, 3, 55, 798, DateTimeKind.Utc).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 9, 3, 55, 798, DateTimeKind.Utc).AddTicks(5184));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Concert");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 7, 12, 42, 164, DateTimeKind.Utc).AddTicks(8156));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 7, 12, 42, 164, DateTimeKind.Utc).AddTicks(8159));
        }
    }
}
