using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addingLocalUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1676), new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1669) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1686), new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1682) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1695), new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1691) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1703), new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1712), new DateTime(2024, 6, 4, 15, 49, 27, 729, DateTimeKind.Local).AddTicks(1708) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9017), new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9026), new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9022) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9034), new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9031) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9042), new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9039) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9050), new DateTime(2024, 6, 4, 15, 47, 42, 632, DateTimeKind.Local).AddTicks(9047) });
        }
    }
}
