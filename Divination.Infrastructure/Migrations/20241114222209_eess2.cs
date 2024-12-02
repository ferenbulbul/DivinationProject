using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eess2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditRequiremt",
                table: "FortuneTellers",
                newName: "CreditRequirement");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 22, 9, 431, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 22, 9, 431, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 22, 9, 431, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 22, 9, 431, DateTimeKind.Local).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 22, 9, 431, DateTimeKind.Local).AddTicks(7170));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditRequirement",
                table: "FortuneTellers",
                newName: "CreditRequiremt");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 20, 25, 865, DateTimeKind.Local).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 20, 25, 865, DateTimeKind.Local).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 20, 25, 865, DateTimeKind.Local).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 20, 25, 865, DateTimeKind.Local).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 1, 20, 25, 865, DateTimeKind.Local).AddTicks(3960));
        }
    }
}
