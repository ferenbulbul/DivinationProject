using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addVotedFortuneTeller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVoted",
                table: "FortuneTellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 15, 40, 56, 12, DateTimeKind.Local).AddTicks(510));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 15, 40, 56, 12, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 15, 40, 56, 12, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 15, 40, 56, 12, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 29, 15, 40, 56, 12, DateTimeKind.Local).AddTicks(570));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVoted",
                table: "FortuneTellers");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 20, 17, 46, 12, 362, DateTimeKind.Local).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 20, 17, 46, 12, 362, DateTimeKind.Local).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 20, 17, 46, 12, 362, DateTimeKind.Local).AddTicks(6150));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 20, 17, 46, 12, 362, DateTimeKind.Local).AddTicks(6150));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 20, 17, 46, 12, 362, DateTimeKind.Local).AddTicks(6150));
        }
    }
}
