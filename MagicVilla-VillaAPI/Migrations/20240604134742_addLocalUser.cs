using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addLocalUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4136), new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4131) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4142), new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4139) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4147), new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4145) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4153), new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4158), new DateTime(2024, 6, 4, 9, 13, 27, 817, DateTimeKind.Local).AddTicks(4156) });
        }
    }
}
