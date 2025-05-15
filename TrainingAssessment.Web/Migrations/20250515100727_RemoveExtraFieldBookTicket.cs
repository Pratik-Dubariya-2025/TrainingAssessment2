using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAssessment.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraFieldBookTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcertTime",
                table: "BookTicket");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "BookTicket");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "BookTicket");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "BookTicket");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 10, 7, 27, 313, DateTimeKind.Utc).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 15, 10, 7, 27, 313, DateTimeKind.Utc).AddTicks(136));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConcertTime",
                table: "BookTicket",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "BookTicket",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "BookTicket",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "BookTicket",
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
    }
}
