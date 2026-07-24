using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "usuario_id",
                schema: "public",
                table: "veiculos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_veiculos_usuario_id",
                schema: "public",
                table: "veiculos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_email",
                schema: "public",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_veiculos_usuarios_usuario_id",
                schema: "public",
                table: "veiculos",
                column: "usuario_id",
                principalSchema: "public",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_veiculos_usuarios_usuario_id",
                schema: "public",
                table: "veiculos");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_veiculos_usuario_id",
                schema: "public",
                table: "veiculos");

            migrationBuilder.DropColumn(
                name: "usuario_id",
                schema: "public",
                table: "veiculos");
        }
    }
}
