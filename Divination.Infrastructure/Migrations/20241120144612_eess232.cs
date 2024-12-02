using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eess232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequirmentCredit",
                table: "FortuneTellers",
                newName: "RequirementCredit");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequirementCredit",
                table: "FortuneTellers",
                newName: "RequirmentCredit");

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
    }
}
