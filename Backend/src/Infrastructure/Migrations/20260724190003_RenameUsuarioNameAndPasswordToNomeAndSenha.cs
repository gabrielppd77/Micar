using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUsuarioNameAndPasswordToNomeAndSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                schema: "public",
                table: "usuarios",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "public",
                table: "usuarios",
                newName: "nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senha",
                schema: "public",
                table: "usuarios",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "nome",
                schema: "public",
                table: "usuarios",
                newName: "name");
        }
    }
}
