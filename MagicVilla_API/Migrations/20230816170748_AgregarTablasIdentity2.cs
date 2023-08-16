using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablasIdentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "AspNetUsers",
                newName: "Nombres");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 16, 14, 7, 48, 464, DateTimeKind.Local).AddTicks(381), new DateTime(2023, 8, 16, 14, 7, 48, 464, DateTimeKind.Local).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 16, 14, 7, 48, 464, DateTimeKind.Local).AddTicks(385), new DateTime(2023, 8, 16, 14, 7, 48, 464, DateTimeKind.Local).AddTicks(384) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "AspNetUsers",
                newName: "Nombre");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 16, 10, 12, 42, 583, DateTimeKind.Local).AddTicks(6045), new DateTime(2023, 8, 16, 10, 12, 42, 583, DateTimeKind.Local).AddTicks(6033) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 16, 10, 12, 42, 583, DateTimeKind.Local).AddTicks(6049), new DateTime(2023, 8, 16, 10, 12, 42, 583, DateTimeKind.Local).AddTicks(6048) });
        }
    }
}
