using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "FortuneTellers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Answers",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 12, 17, 30, 27, 998, DateTimeKind.Local).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 12, 17, 30, 27, 998, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 12, 17, 30, 27, 998, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 12, 17, 30, 27, 998, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Akrabağlık İlişkileri", new DateTime(2024, 11, 12, 17, 30, 27, 998, DateTimeKind.Local).AddTicks(2890) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "FortuneTellers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Answers");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 24, 20, 3, 58, 430, DateTimeKind.Local).AddTicks(3170));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 24, 20, 3, 58, 430, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 24, 20, 3, 58, 430, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 24, 20, 3, 58, 430, DateTimeKind.Local).AddTicks(3230));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryName", "CreatedDate" },
                values: new object[] { "Akrağbağlık İlişkileri", new DateTime(2024, 10, 24, 20, 3, 58, 430, DateTimeKind.Local).AddTicks(3230) });
        }
    }
}
