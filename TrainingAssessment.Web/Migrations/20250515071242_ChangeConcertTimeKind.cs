using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAssessment.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConcertTimeKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConcertTime",
                table: "Concert",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConcertTime",
                table: "Concert",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 4, 32, 43, 977, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 4, 32, 43, 977, DateTimeKind.Utc).AddTicks(2347));
        }
    }
}
