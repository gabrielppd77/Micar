using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "public",
                table: "veiculos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_veiculos_user_id",
                schema: "public",
                table: "veiculos",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_veiculos_users_user_id",
                schema: "public",
                table: "veiculos",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_veiculos_users_user_id",
                schema: "public",
                table: "veiculos");

            migrationBuilder.DropIndex(
                name: "ix_veiculos_user_id",
                schema: "public",
                table: "veiculos");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "public",
                table: "veiculos");
        }
    }
}
