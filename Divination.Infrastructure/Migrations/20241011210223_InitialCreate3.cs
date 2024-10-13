using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientsss_AspNetUsers_Id",
                table: "Clientsss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientsss",
                table: "Clientsss");

            migrationBuilder.RenameTable(
                name: "Clientsss",
                newName: "Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4d1c008a-56a8-4460-ad9e-529f634a92e1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_Id",
                table: "Clients",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_Id",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Clientsss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientsss",
                table: "Clientsss",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6e016820-ac6f-4de8-bae5-9c4949d4656c");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientsss_AspNetUsers_Id",
                table: "Clientsss",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
