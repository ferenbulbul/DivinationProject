using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eess23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditRequirement",
                table: "FortuneTellers",
                newName: "TotalCredit");

            migrationBuilder.AddColumn<int>(
                name: "RequirmentCredit",
                table: "FortuneTellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 2, 3, 44, 836, DateTimeKind.Local).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 2, 3, 44, 836, DateTimeKind.Local).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 2, 3, 44, 836, DateTimeKind.Local).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 2, 3, 44, 836, DateTimeKind.Local).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 2, 3, 44, 836, DateTimeKind.Local).AddTicks(4140));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequirmentCredit",
                table: "FortuneTellers");

            migrationBuilder.RenameColumn(
                name: "TotalCredit",
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
    }
}
