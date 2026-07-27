using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableManutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "manutencao_id",
                schema: "public",
                table: "registros_odometro",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "manutencoes",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    veiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    odometro_vencimento = table.Column<int>(type: "integer", nullable: true),
                    data_vencimento = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_manutencoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_manutencoes_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "public",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_registros_odometro_manutencao_id",
                schema: "public",
                table: "registros_odometro",
                column: "manutencao_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_manutencoes_veiculo_id",
                schema: "public",
                table: "manutencoes",
                column: "veiculo_id");

            migrationBuilder.AddForeignKey(
                name: "fk_registros_odometro_manutencoes_manutencao_id",
                schema: "public",
                table: "registros_odometro",
                column: "manutencao_id",
                principalSchema: "public",
                principalTable: "manutencoes",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_registros_odometro_manutencoes_manutencao_id",
                schema: "public",
                table: "registros_odometro");

            migrationBuilder.DropTable(
                name: "manutencoes",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_registros_odometro_manutencao_id",
                schema: "public",
                table: "registros_odometro");

            migrationBuilder.DropColumn(
                name: "manutencao_id",
                schema: "public",
                table: "registros_odometro");
        }
    }
}
