using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 10, 22, 9, 36, 478, DateTimeKind.Local).AddTicks(7820), new DateTime(2023, 8, 10, 22, 9, 36, 478, DateTimeKind.Local).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 10, 22, 9, 36, 478, DateTimeKind.Local).AddTicks(7825), new DateTime(2023, 8, 10, 22, 9, 36, 478, DateTimeKind.Local).AddTicks(7824) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 25, 18, 0, 59, 368, DateTimeKind.Local).AddTicks(6066), new DateTime(2023, 7, 25, 18, 0, 59, 368, DateTimeKind.Local).AddTicks(6054) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 7, 25, 18, 0, 59, 368, DateTimeKind.Local).AddTicks(6069), new DateTime(2023, 7, 25, 18, 0, 59, 368, DateTimeKind.Local).AddTicks(6069) });
        }
    }
}
